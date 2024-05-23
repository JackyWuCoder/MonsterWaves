using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; private set; }

    [Header("Ammo")]
    public TextMeshProUGUI magazineAmmoUI;
    public TextMeshProUGUI totalAmmoUI;
    public Image ammoTypeUI;

    [Header("Weapon")]
    public Image activeWeaponUI;
    public Image unActiveWeaponUI;

    public Sprite emptySlot;

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

    private void Update()
    {
        Weapon activeWeapon = WeaponManager.Instance.activeWeaponSlot?.GetComponentInChildren<Weapon>();
        Weapon unActiveWeapon = GetUnActiveWeaponSlot()?.GetComponentInChildren<Weapon>();

        if (activeWeapon != null)
        {
            magazineAmmoUI.text = $"{activeWeapon.bulletsLeft / activeWeapon.bulletsPerBurst}";
            totalAmmoUI.text = $"{WeaponManager.Instance.CheckAmmoLeft(activeWeapon.thisWeaponModel)}";

            Weapon.WeaponModel model = activeWeapon.thisWeaponModel;
            ammoTypeUI.sprite = GetAmmoSprite(model);
            activeWeaponUI.sprite = GetWeaponSprite(model);

            unActiveWeaponUI.sprite = unActiveWeapon != null ? GetWeaponSprite(unActiveWeapon.thisWeaponModel) : emptySlot;
        }
        else
        {
            magazineAmmoUI.text = "";
            totalAmmoUI.text = "";
            ammoTypeUI.sprite = emptySlot;
            activeWeaponUI.sprite = emptySlot;
            unActiveWeaponUI.sprite = emptySlot;
        }
    }

    private Sprite GetWeaponSprite(Weapon.WeaponModel model)
    {
        string resourceName = model switch
        {
            Weapon.WeaponModel.Pistol1 => "Deagle_Weapon",
            Weapon.WeaponModel.M4 => "M4_Weapon",
            _ => null
        };

        if (string.IsNullOrEmpty(resourceName)) return null;

        GameObject resource = Resources.Load<GameObject>(resourceName);
        return resource != null ? resource.GetComponent<SpriteRenderer>().sprite : null;
    }

    private Sprite GetAmmoSprite(Weapon.WeaponModel model)
    {
        string resourceName = model switch
        {
            Weapon.WeaponModel.Pistol1 => "Pistol_Ammo",
            Weapon.WeaponModel.M4 => "Rifle_Ammo",
            _ => null
        };

        if (string.IsNullOrEmpty(resourceName)) return null;

        GameObject resource = Resources.Load<GameObject>(resourceName);
        return resource != null ? resource.GetComponent<SpriteRenderer>().sprite : null;
    }

    private GameObject GetUnActiveWeaponSlot()
    {
        foreach (GameObject weaponSlot in WeaponManager.Instance.weaponSlots)
        {
            if (weaponSlot != WeaponManager.Instance.activeWeaponSlot)
            {
                return weaponSlot;
            }
        }
        return null; // In case all slots are the same, return null.
    }
}
