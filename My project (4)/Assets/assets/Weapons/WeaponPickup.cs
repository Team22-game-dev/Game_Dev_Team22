using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{

    [SerializeField] private GameObject weaponPrefab; // prefab for the weapon. Drag and drop the desired weapon here in the unity editor
    [SerializeField] private float pickupRadius = 2.0f; // radius from weapon in which the weapon can be picked up
    [SerializeField] private KeyCode pickupKey = KeyCode.E; // key to pick up weapon
    [SerializeField] private LayerMask playerLayer; // Layer for the playerLayer

    private bool isPlayerInRange = false;
    private SphereCollider sphereCollider;

    // Start is called before the first frame update
    void Start()
    {
        // add sphere collider to weapon object for its pickup radius
        sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
        sphereCollider.radius = pickupRadius;
        sphereCollider.enabled = true;

        // Set collider to weapon pickup layer
        gameObject.layer = LayerMask.NameToLayer("Weapon");
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if player is in rang and would like to pickup the weapon
        if(isPlayerInRange && Input.GetKeyDown(pickupKey))
        {
            PickupWeapon();
        }
        
    }

    private void OnTriggerEnter(Collider other) // This is the collider that triggers the pickup context such that the action button will pikup the weapon
    {
        Debug.Log("Collider Triggered");
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            isPlayerInRange = true;
            Debug.Log("Player is in range to pick up the weapon.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            isPlayerInRange = false;
            Debug.Log("Player has left range to pickup weapon.");
        }
    }

    private void PickupWeapon()
    {
        Debug.Log("Picking Up Weapon!");
        // disable sphereCollider
        sphereCollider.enabled = false;
        // If player has no other weapon equipped, equip this weapon and its sheath model ( sheath model is not seen when found in open world)
        Debug.Log("Sphere collider disabled");
        if(MC_EquippedMeleWeapon.Instance.hasMeleWeaponEquipped())
        {
            Debug.Log("To Inventory");
            // send to characters weapon Inventory (not yet created)
        }
        else
        {
            Debug.Log("Equip Now!");
            // equip this weapon
            MC_EquippedMeleWeapon.Instance.StartEquipMeleWeaponCoroutine(weaponPrefab);

        }
        // else put into enventory data structure (an array list)
    }

    public void DropWeapon()
    {
        sphereCollider.enabled = true;
    }
}
