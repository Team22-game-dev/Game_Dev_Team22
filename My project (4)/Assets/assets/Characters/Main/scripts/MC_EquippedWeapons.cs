using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A copy of the equipped weapons parameters local to the character

public class MC_EquippedMeleWeapon : MonoBehaviour
{

    // use singleton since only one mele weapon should be equipped at a time
    private static MC_EquippedMeleWeapon _instance; // the local private _instance
    public static MC_EquippedMeleWeapon Instance => _instance;

    [SerializeField] private GameObject equippedMeleWeapon;



    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;

        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator EquipMeleWeapon(GameObject meleWeaponToEquip)
    {
        Debug.Log("attempting to Equip Weapon");
        if (meleWeaponToEquip == null)
        {
            Debug.LogWarning("Attempted to equip a null weapon.");
            yield break; // Early exit if the weapon is null
        }

        // switch weapon target instantaneously so long as attac annimation is not playing
        string animationName;
        float startTime = Time.time;
        float weaponSwitchTimeLimit = 0.5f;
        do
        {
            animationName = MC_AnimationManager.Instance.GetCurrentAnimationName();
            if(animationName == "transition") yield return new WaitForSeconds(.033f);

            float diff = Time.time - startTime;

            if(diff > weaponSwitchTimeLimit)
            {
                Debug.LogWarning("Weapon switch timed out while waiting for animation to finish.\nlast animation name was " + animationName + " time difference: " + diff);
                yield break;
            }
        }
        while(animationName == "transition");


        if(!animationName.StartsWith("attack_"))
        {
            Debug.Log("All good to equip weapon");
            equippedMeleWeapon = meleWeaponToEquip;
        }

    }

    public void StartEquipMeleWeaponCoroutine(GameObject meleWeaponToEquip)
    {
        StartCoroutine(EquipMeleWeapon(meleWeaponToEquip));
    }

    public GameObject GetEquippedWeapon()
    {
        return equippedMeleWeapon;
    }

    public bool hasMeleWeaponEquipped()
    {
        Debug.Log("Checking For Equipped Weapon");
        return equippedMeleWeapon != null;
    }
}
