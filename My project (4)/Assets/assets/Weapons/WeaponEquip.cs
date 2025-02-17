using System.Collections;
using UnityEngine;

/*
 component applied to weapons to determine their states and parenting in the games context.
 */

public class WeaponEquip : MonoBehaviour
{
    [SerializeField] private WeaponData weaponData; // Reference to the weapon's data

    private bool isDrawn = false; // Tracks whether the weapon is drawn

    // Start is called before the first frame update
    void Start()
    {
        UpdateWeaponState();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(ToggleEquip());
        }
        else if (Input.GetMouseButtonDown(0))
        {
            switch (weaponData.GetWeaponState())
            {
                case WeaponData.State.Equipped:
                    DrawWeapon();
                    break;
                case WeaponData.State.Drawn:
                    Debug.Log("Attack!"); // place holder for calling attack animation
                    break;
                default:
                    // Debug.LogError("Tried to attack or draw weapon " + WeaponData.WeaponName + " when nont drawn or equipped!");
                    break;
            }
        }
    }

    IEnumerator ToggleEquip()
    {
        // If weapon is drawn, sheath it before unequipping
        if (weaponData.GetWeaponState() == WeaponData.State.Equipped && isDrawn)
        {
            SheathWeapon();
            yield return new WaitUntil(() => !isDrawn); // Wait until weapon is sheathed
        }

        // Toggle equipped state
        if (weaponData.GetWeaponState() == WeaponData.State.Equipped)
            weaponData.SetWeaponState(WeaponData.State.Inventory); // Unequip weapon
        else
            weaponData.SetWeaponState(WeaponData.State.Equipped); // Equip weapon

        UpdateWeaponState();
    }

    void DrawWeapon()
    {
        if (weaponData.GetWeaponState() != WeaponData.State.Equipped || isDrawn || weaponData.Weapon == null || weaponData == null) return;

        // Play draw animation
        MC_AnimationManager.Instance.SetTrigger(weaponData.DrawAnimation);

        // Reparenting is handled by the animation event calling Draw_SheathWeapon
    }

    void SheathWeapon()
    {
        if (weaponData.GetWeaponState() != WeaponData.State.Equipped || !isDrawn || weaponData.Weapon == null || weaponData == null) return;

        MC_AnimationManager.Instance.SetTrigger(weaponData.SheathAnimation);
    }

    public void Draw_SheathWeapon()
    {
        Debug.Log("Draw_SheathWeapon called");
        if (weaponData.Weapon != null && weaponData != null) // Safety checks
        {
            if (!isDrawn)
            {
                // Parent weapon to the correct hand(s) based on weaponData
                weaponData.Weapon.transform.SetParent(weaponData.ActionBone);
            }
            else
            {
                // Parent weapon back to the sheath
                weaponData.Weapon.transform.SetParent(weaponData.SheathedBone);
            }

            // Reset local position and rotation
            weaponData.Weapon.transform.localPosition = Vector3.zero;
            weaponData.Weapon.transform.localRotation = Quaternion.identity;

            isDrawn = !isDrawn; // Toggle drawn state
        }
        else
        {
            Debug.LogError("Weapon or Weapon Data not found in Draw_SheathWeapon");
        }
    }
    void UpdateWeaponState()
    {
        switch (weaponData.GetWeaponState())
        {
            case WeaponData.State.Inventory:
                // Handle inventory state logic
                break;

            case WeaponData.State.Equipped:
                if (weaponData.Weapon != null)
                {
                    // Reset local transform to prevent unexpected offsets
                    weaponData.Weapon.transform.localPosition = Vector3.zero;
                    weaponData.Weapon.transform.localRotation = Quaternion.identity;
                    weaponData.Weapon.transform.localScale = Vector3.one;

                    // Parent to the sheath bone
                    weaponData.Weapon.transform.SetParent(weaponData.SheathedBone);

                    // Align position and rotation with the sheath bone
                    weaponData.Weapon.transform.localPosition = Vector3.zero; // Adjust as needed
                    weaponData.Weapon.transform.localRotation = Quaternion.Euler(-91.3f, -0.075f, 90.03f); // Adjust as needed
                }
                break;

            case WeaponData.State.Drawn:
                // Handle drawn state logic
                break;

            case WeaponData.State.Dropped:
                // Handle dropped state logic if needed
                break;
        }
    }
}
