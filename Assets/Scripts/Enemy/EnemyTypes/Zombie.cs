using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie : Enemy
{
    private ZombieStateMachine stateMachine;
    private Animator animator;

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
        if (health == 0)
        {
            animator.Play("Z_FallingBack");
        }
    }
}
