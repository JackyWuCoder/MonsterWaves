using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private bool isActiveWeapon;

    // Shooting
    [Header("Shooting")]
    [SerializeField] bool isShooting;
    [SerializeField] bool readyToShoot;
    private bool allowReset = true;
    [SerializeField] private float shootingDelay = 0f;

    // Bursting
    [Header("Bursting")]
    public int bulletsPerBurst = 3;
    public int burstBulletsLeft;

    // Spread
    [Header("Spread")]
    [SerializeField] private float spreadIntensity;

    // Bullet
    [Header("Bullet")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private float bulletVelocity = 500.0f;
    [SerializeField] private float bulletPrefabLifeTime = 3.0f; // seconds

    [SerializeField] private GameObject muzzleEffect;
    internal Animator animator;

    // Reloading
    [SerializeField] float reloadTime;
    public int magazineSize;
    [SerializeField] int bulletsLeft;
    [SerializeField] bool isReloading;

    // Weapon spawn position and rotation relative to the player.
    [Header("Player Weapon Transform Values")]
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private Vector3 spawnRotation;

    public enum WeaponModel
    { 
        Pistol1,
        M4
    }

    public WeaponModel thisWeaponModel;

    public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }

    [SerializeField] private ShootingMode currentShootingMode;

    private void Awake()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;
        animator = GetComponent<Animator>();

        bulletsLeft = magazineSize;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isActiveWeapon) 
        {
            GetComponent<Outline>().enabled = false;
            if ((bulletsLeft == 0) && isShooting)
            {
                SoundManager.Instance.emptyMagazineSoundPistol1.Play();
            }
            if (currentShootingMode == ShootingMode.Auto)
            {
                // Holding Down Left mouse Button.
                isShooting = Input.GetKey(KeyCode.Mouse0);
            }
            else if (currentShootingMode == ShootingMode.Single ||
                currentShootingMode == ShootingMode.Burst)
            {
                // Clicking Left Mouse Button Once.
                isShooting = Input.GetKeyDown(KeyCode.Mouse0);
            }
            if ((Input.GetKeyDown(KeyCode.R)) && (bulletsLeft < magazineSize) && (!isReloading))
            {
                Reload();
            }
            /*
            // Auto reload when magazine is empty.
            if (readyToShoot && !isShooting && !isReloading && (bulletsLeft <= 0))
            {
                Reload();
            }
            */
            if (readyToShoot && isShooting && (bulletsLeft > 0))
            {
                burstBulletsLeft = bulletsPerBurst;
                FireWeapon();
            }
        }
    }

    private void FireWeapon()
    {
        bulletsLeft--;

        muzzleEffect.GetComponent<ParticleSystem>().Play();
        animator.SetTrigger("Recoil");
        SoundManager.Instance.PlayShootingSound(thisWeaponModel);

        readyToShoot = false;
        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;
        // Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        // Pointing the bullet to face the shooting direction.
        bullet.transform.forward = shootingDirection;
        // Shoot the bullet
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);
        // Destroy the bullet after some time
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));
        // Checking if we are done shooting
        if (allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }
        // Burst Mode
        // We already shot once before this check
        if (currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1)
        {
            burstBulletsLeft--;
            Invoke("FireWeapon", shootingDelay);
        }
    }

    private void Reload()
    {
        SoundManager.Instance.PlayReloadSound(thisWeaponModel);
        animator.SetTrigger("Reload");
        isReloading = true;
        Invoke("ReloadCompleted", reloadTime);
    }

    private void ReloadCompleted()
    {
        isReloading = false;
        bulletsLeft = magazineSize;
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }

    public Vector3 CalculateDirectionAndSpread()
    {
        // Shooting from the middle of the screen to check where are we pointing at.
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            // Hitting Something
            targetPoint = hit.point;
        }
        else
        {
            // Shooting at the air
            targetPoint = ray.GetPoint(100);
        }
        Vector3 direction = targetPoint - bulletSpawn.position;
        float x = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        // Returning the shooting direction and spread.
        return direction + new Vector3(x, y, 0);
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }

    public Vector3 GetSpawnPosition()
    {
        return spawnPosition;
    }

    public Vector3 GetSpawnRotation()
    {
        return spawnRotation;
    }

    public bool GetIsActiveWeapon() 
    {
        return isActiveWeapon;
    }

    public void SetIsActiveWeapon(bool active)
    {
        isActiveWeapon = active;
    }
}
