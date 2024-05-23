using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberMonsterPatrolState : PatrolState
{
    [SerializeField] private float patrolTime = 10f;
    [SerializeField] private float detectionAreaRadius = 18f;
    [SerializeField] private float patrolSpeed = 2f;

    private Transform player;
    private float timer;

    public override void Perform()
    {
        base.Perform();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy.agent.speed = patrolSpeed;
        timer = 0;
        if (SoundManager.Instance.cyberMonsterChannel.isPlaying == false)
        {
            SoundManager.Instance.cyberMonsterChannel.clip = SoundManager.Instance.zombieWalking;
            SoundManager.Instance.cyberMonsterChannel.PlayDelayed(1f);
        }
        if (enemy.CanSeePlayer())
        {
            stateMachine.ChangeState(new CyberMonsterAttackState());
        }
        // Transition to Idle State
        timer += Time.deltaTime;
        if (timer > 0)
        {
            enemy.animator.SetBool("isPatrolling", false);
            stateMachine.ChangeState(new CyberMonsterIdleState());
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
        base.Exit();
        // Stop the agent
        enemy.agent.SetDestination(enemy.agent.transform.position);
        SoundManager.Instance.zombieChannel.Stop();
    }
}
