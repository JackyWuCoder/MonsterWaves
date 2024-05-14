using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; set; }

    public List<GameObject> weaponSlots;
    public GameObject activeWeaponSlot;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        activeWeaponSlot = weaponSlots[0];
    }

    private void Update()
    {
        foreach (GameObject weaponSlot in weaponSlots)
        {
            if (weaponSlot == activeWeaponSlot)
            {
                weaponSlot.SetActive(true);
            }
            else
            {
                weaponSlot.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchActiveSlot(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchActiveSlot(1);
        }
    }

    public void PickupWeapon(GameObject pickedupWeapon)
    {
        AddWeaponIntoActiveSlot(pickedupWeapon);
    }

    private void AddWeaponIntoActiveSlot(GameObject pickedUpWeapon)
    {
        DropCurrentWeapon(pickedUpWeapon);
        // Revert weapon back to default layer
        ChangeLayers(pickedUpWeapon, "WeaponRender");
        pickedUpWeapon.transform.SetParent(activeWeaponSlot.transform, false);
        Weapon weapon = pickedUpWeapon.GetComponent<Weapon>();
        pickedUpWeapon.transform.localPosition = new Vector3(weapon.GetSpawnPosition().x, weapon.GetSpawnPosition().y, weapon.GetSpawnPosition().z);
        pickedUpWeapon.transform.localRotation = Quaternion.Euler(weapon.GetSpawnRotation().x, weapon.GetSpawnRotation().y, weapon.GetSpawnRotation().z);
        weapon.SetIsActiveWeapon(true);
        weapon.animator.enabled = true;
    }

    private void DropCurrentWeapon(GameObject pickedupWeapon)
    {
        if (activeWeaponSlot.transform.childCount > 0)
        {
            var weaponToDrop = activeWeaponSlot.transform.GetChild(0).gameObject;
            // Revert weapon back to default layer
            ChangeLayers(weaponToDrop, "Default");
            weaponToDrop.GetComponent<Weapon>().SetIsActiveWeapon(false);
            weaponToDrop.GetComponent<Weapon>().animator.enabled = false;
            weaponToDrop.transform.SetParent(pickedupWeapon.transform.parent);
            weaponToDrop.transform.localPosition = pickedupWeapon.transform.localPosition;
            weaponToDrop.transform.localRotation = pickedupWeapon.transform.localRotation;
        }
    }

    public void SwitchActiveSlot(int slotNumber)
    {
        if (activeWeaponSlot.transform.childCount > 0)
        {
            Weapon currentWeapon = activeWeaponSlot.transform.GetChild(0).GetComponent<Weapon>();
            currentWeapon.SetIsActiveWeapon(false);
        }
        activeWeaponSlot = weaponSlots[slotNumber];
        if (activeWeaponSlot.transform.childCount > 0)
        {
            Weapon newWeapon = activeWeaponSlot.transform.GetChild(0).GetComponent<Weapon>();
            newWeapon.SetIsActiveWeapon(true);
        }
    }

    public void ChangeLayers(GameObject weapon, string layerName)
    {
        // Create a queue to store GameObjects to be processed
        Queue<Transform> queue = new Queue<Transform>();

        // Enqueue the root object
        queue.Enqueue(weapon.transform);

        // Continue until all objects in the queue have been processed
        while (queue.Count > 0)
        {
            // Dequeue the next object from the queue
            Transform currentTransform = queue.Dequeue();
            GameObject currentObj = currentTransform.gameObject;

            // Change the layer of the current object
            currentObj.layer = LayerMask.NameToLayer(layerName);

            // Enqueue all children of the current object
            foreach (Transform child in currentTransform)
            {
                queue.Enqueue(child);
            }
        }
    }
}
