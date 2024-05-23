using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberMonsterChaseState : BaseState
{
    [SerializeField] private float chaseSpeed = 3f;
    [SerializeField] private float stopChasingDistance = 21f;
    [SerializeField] private float attackingDistance = 10f;

    public override void Enter()
    {
        enemy.agent.speed = chaseSpeed;
    }

    public override void Perform()
    {
        if (SoundManager.Instance.cyberMonsterChannel.isPlaying == false)
        {
            SoundManager.Instance.cyberMonsterChannel.PlayOneShot(SoundManager.Instance.cyberMonsterChase);
        }
        enemy.agent.SetDestination(enemy.player.transform.position);
        enemy.animator.transform.LookAt(enemy.player.transform);

        float distanceFromPlayer = Vector3.Distance(enemy.player.transform.position, enemy.animator.transform.position);

        // Check if agent should stop chasing
        if (distanceFromPlayer > stopChasingDistance)
        {
            enemy.animator.SetBool("isChasing", false);
            stateMachine.ChangeState(new CyberMonsterPatrolState());
        }

        // Check if agent should attack
        if (distanceFromPlayer < attackingDistance)
        {
            enemy.animator.SetBool("isAttacking", true);
            stateMachine.ChangeState(new CyberMonsterAttackState());
        }
    }

    public override void Exit()
    {
        // Stops the agent from moving
        enemy.agent.SetDestination(enemy.animator.transform.position);
        SoundManager.Instance.cyberMonsterChannel.Stop();
    }
}
