using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    private ZombieStateMachine stateMachine;

    // Only for debugging purposes.
    [Header("Debugging")]
    [SerializeField] protected string currentState;

    protected override void Start()
    {
        base.Start();
        stateMachine = GetComponent<ZombieStateMachine>();
        stateMachine.Initialize();
    }

    protected override void Update()
    {
        base.Update();
        currentState = stateMachine.GetActiveState().ToString();
    }
}
