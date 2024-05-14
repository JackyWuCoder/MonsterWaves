using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CyberMonster : Enemy
{
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

    public void Shoot()
    {
        // Store reference to the gun barrel.
        Transform gunBarrel = this.gunBarrel;
        // Instantiate a new bullet.
        GameObject bullet = Instantiate(bulletPrefab, gunBarrel.position, Quaternion.identity);
        // Calculate the direction to the player.
        Vector3 shootDirection = (player.transform.position - gunBarrel.transform.position).normalized;
        // Add force to the rigidbody of the bullet.
        bullet.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(Random.Range(-3.0f, 3.0f), Vector3.up) * shootDirection * bulletVelocity;
        animator.Play("Shoot", 1);
    }
}
