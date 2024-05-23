using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberMonsterIdleState : BaseState
{
    [SerializeField] private float idleTime = 0f;
    [SerializeField] private float detectionAreaRadius = 18f;

    private float timer;
    private Transform player;

    public override void Enter()
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void Perform()
    {
        // Transition to Patrol State
        timer += Time.deltaTime;
        if (timer > idleTime)
        {
            enemy.animator.SetBool("isPatrolling", true);
            stateMachine.ChangeState(new CyberMonsterPatrolState());
        }

        // Transition to Chase State
        float distanceFromPlayer = Vector3.Distance(player.position, enemy.animator.transform.position);
        if (distanceFromPlayer < detectionAreaRadius)
        {
            enemy.animator.SetBool("isChasing", true);
            stateMachine.ChangeState(new CyberMonsterChaseState());
        }
    }

    public override void Exit()
    {
        
    }
}
