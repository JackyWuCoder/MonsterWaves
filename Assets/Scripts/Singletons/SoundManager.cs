using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Weapon;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }

    public AudioSource shootingSoundPistol1;
    public AudioSource reloadingSoundPistol1;
    public AudioSource emptyMagazineSoundPistol1;

    public AudioSource shootingSoundM4;
    public AudioSource reloadingSoundM4;

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

    public void PlayShootingSound(WeaponModel weaponModel)
    {
        switch (weaponModel)
        {
            case WeaponModel.Pistol1:
                shootingSoundPistol1.Play();
                break;
            case WeaponModel.M4:
                shootingSoundM4.Play();
                break;
        }
    }

    public void PlayReloadSound(WeaponModel weaponModel)
    {
        switch (weaponModel)
        {
            case WeaponModel.Pistol1:
                reloadingSoundPistol1.Play();
                break;
            case WeaponModel.M4:
                reloadingSoundM4.Play();
                break;
        }
    }
}
