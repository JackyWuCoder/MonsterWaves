using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackState : AttackState
{
    private Zombie zombieEnemy;
    public float stopAttackingDistance = 2.5f;

    public override void Enter()
    {
        zombieEnemy = (Zombie)enemy;
    }

    public override void Perform()
    {
        base.Perform();
        if (SoundManager.Instance.zombieChannel.isPlaying == false)
        {
            SoundManager.Instance.zombieChannel.PlayOneShot(SoundManager.Instance.zombieAttack);
        }
        // Check if agent should stop attacking
        float distanceFromPlayer = Vector3.Distance(player.position, enemy.animator.transform.position);
        if (distanceFromPlayer > stopAttackingDistance)
        {
            enemy.animator.SetBool("isAttacking", false);
        }
        // Enemy can see the player.
        if (!enemy.CanSeePlayer())
        {
            if (losePlayerTimer > 8)
            {
                // Change to the search state
                // stateMachine.ChangeState(new ZombiePatrolState());
                stateMachine.ChangeState(new ZombieSearchState());
            }
        }
    }

    public override void Exit()
    {
        SoundManager.Instance.zombieChannel.Stop();
    }
}
