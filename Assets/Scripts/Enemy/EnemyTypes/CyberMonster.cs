using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CyberMonster : Enemy
{
    private CyberMonsterStateMachine stateMachine;
    private Animator animator;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed = 40.0f;

    [Header("Weapon Values")]
    [SerializeField] private Transform gunBarrel;
    [Range(0.1f, 10.0f)]
    [SerializeField] private float fireRate;

    // Only for debugging purposes.
    [Header("Debugging")]
    [SerializeField] protected string currentState;

    protected override void Start()
    {
        base.Start();
        stateMachine = GetComponent<CyberMonsterStateMachine>();
        stateMachine.Initialize();
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        currentState = stateMachine.GetActiveState().ToString();
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
        Instantiate(bullet, gunBarrel.position, transform.rotation);
        // Calculate the direction to the player.
        Vector3 shootDirection = (player.transform.position - gunBarrel.transform.position).normalized;
        // Add force to the rigidbody of the bullet.
        bullet.GetComponent<Rigidbody>().velocity = shootDirection * bulletSpeed;
        animator.Play("Shoot", 1);
        Debug.Log("CyberMonster is Shooting.");
    }
}
