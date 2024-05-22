using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie : Enemy
{
    private ZombieStateMachine stateMachine;
    [SerializeField] private ZombieHand zombieLeftHand;
    [SerializeField] private ZombieHand zombieRightHand;
    [SerializeField] private int zombieDamage = 5;

    protected override void Start()
    {
        base.Start();
        stateMachine = GetComponent<ZombieStateMachine>();
        stateMachine.Initialize();
        zombieLeftHand.SetDamage(zombieDamage);
        zombieRightHand.SetDamage(zombieDamage);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if (health <= 0)
        {
            int randomValue = Random.Range(0, 2); // 0,1
            if (randomValue == 0)
            {
                animator.SetTrigger("DIE1");
            }
            else
            {
                animator.SetTrigger("DIE2");
            }
            // Play the death animation
            //animator.Play("Death");
            // Start a coroutin to despawn the enemy after a delay
            StartCoroutine(DespawnAfterDelay());
        }
    }
}
