using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CyberMonster : Enemy
{
    [SerializeField] private int gunDamage = 10;

    private CyberMonsterStateMachine stateMachine;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletVelocity = 40.0f;

    [Header("Weapon Values")]
    [SerializeField] private Transform gunBarrel;
    [Range(0.1f, 10.0f)]
    [SerializeField] private float fireRate;

    protected override void Start()
    {
        base.Start();
        stateMachine = GetComponent<CyberMonsterStateMachine>();
        stateMachine.Initialize();
    }

    protected override void Update()
    {
        base.Update();
        if (health == 0)
        {
            animator.Play("Death", 1);
        }
    }

    public float GetFireRate()
    {
        return fireRate;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if (health <= 0)
        {
             animator.SetTrigger("DIE");
        }
        SoundManager.Instance.cyberMonsterChannel.PlayOneShot(SoundManager.Instance.cyberMonsterDeath);
        // Game Over
        // Respawn Player
        // Play the death animation
        //animator.Play("Death");
        // Start a coroutin to despawn the enemy after a delay
        StartCoroutine(DespawnAfterDelay());
    }

    public void Shoot()
    {
        // Store reference to the gun barrel.
        Transform gunBarrel = this.gunBarrel;
        // Instantiate a new bullet.
        GameObject bullet = Instantiate(bulletPrefab, gunBarrel.position, Quaternion.identity);
        Bullet bul = bullet.GetComponent<Bullet>();
        bul.SetBulletDamage(gunDamage);
        // Calculate the direction to the player.
        Vector3 shootDirection = (player.transform.position - gunBarrel.transform.position).normalized;
        // Add force to the rigidbody of the bullet.
        bullet.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(Random.Range(-3.0f, 3.0f), Vector3.up) * shootDirection * bulletVelocity;
        animator.Play("Shoot", 1);
    }
}
