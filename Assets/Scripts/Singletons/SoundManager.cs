using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Weapon;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }

    public AudioSource shootingChannel;

    public AudioClip shotSoundPistol1;
    public AudioClip shotSoundM4;

    public AudioSource reloadingSoundPistol1;
    public AudioSource reloadingSoundM4;

    public AudioSource emptyMagazineSoundPistol1;

    public AudioSource zombieChannel;
    public AudioClip zombieWalking;
    public AudioClip zombieChase;
    public AudioClip zombieAttack;
    public AudioClip zombieDeath;

    public AudioSource cyberMonsterChannel;
    public AudioClip cyberMonsterWalking;
    public AudioClip cyberMonsterChase;
    public AudioClip cyberMonsterAttack;
    public AudioClip cyberMonsterDeath;


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
                shootingChannel.PlayOneShot(shotSoundPistol1);
                break;
            case WeaponModel.M4:
                shootingChannel.PlayOneShot(shotSoundM4);
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
