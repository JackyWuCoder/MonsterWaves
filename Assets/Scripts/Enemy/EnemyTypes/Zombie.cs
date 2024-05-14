using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie : Enemy
{
    private ZombieStateMachine stateMachine;

    protected override void Start()
    {
        base.Start();
        stateMachine = GetComponent<ZombieStateMachine>();
        stateMachine.Initialize();
    }
}
