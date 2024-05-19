using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie : Enemy
{
    private ZombieStateMachine stateMachine;
    [SerializeField] private ZombieHand zombieHand;
    [SerializeField] private int zombieDamage = 5;

    protected override void Start()
    {
        base.Start();
        stateMachine = GetComponent<ZombieStateMachine>();
        stateMachine.Initialize();

        zombieHand.SetDamage(zombieDamage);
    }
}
