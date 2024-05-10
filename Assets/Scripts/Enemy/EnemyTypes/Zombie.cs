using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie : Enemy
{
    private ZombieStateMachine stateMachine;
    private Animator animator;


    // Only for debugging purposes.
    [Header("Debugging")]
    [SerializeField] protected string currentState;

    protected override void Start()
    {
        base.Start();
        stateMachine = GetComponent<ZombieStateMachine>();
        stateMachine.Initialize();
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        currentState = stateMachine.GetActiveState().ToString();
        if (health == 0)
        {
            animator.Play("Z_FallingBack");
        }
    }
}
