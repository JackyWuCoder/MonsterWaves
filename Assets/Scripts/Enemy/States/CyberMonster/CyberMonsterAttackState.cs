using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberMonsterAttackState : AttackState
{
    private CyberMonster cyberEnemy;
    private float shotTimer;
    public float stopAttackingDistance = 2.5f;

    public override void Enter()
    {
        cyberEnemy = (CyberMonster)enemy;
    }

    public override void Perform()
    {
        base.Perform();
        if (SoundManager.Instance.cyberMonsterChannel.isPlaying == false)
        {
            SoundManager.Instance.cyberMonsterChannel.PlayOneShot(SoundManager.Instance.cyberMonsterAttack);
        }
        // Check if agent should stop attacking
        float distanceFromPlayer = Vector3.Distance(player.position, enemy.animator.transform.position);
        if (distanceFromPlayer > stopAttackingDistance)
        {
            enemy.animator.SetBool("isAttacking", false);
        }
        // Enemy can see the player.
        if (enemy.CanSeePlayer())
        {
            // Increment the shotTimer;
            shotTimer += Time.deltaTime;
            if (shotTimer > cyberEnemy.GetFireRate())
            {
                cyberEnemy.Shoot();
                shotTimer = 0;
            }
        }
        else
        {
            if (losePlayerTimer > 8)
            {
                // Change to the search state
                // stateMachine.ChangeState(new CyberMonsterPatrolState());
                stateMachine.ChangeState(new CyberMonsterSearchState());
            }
        }
    }

    public override void Exit()
    {
        SoundManager.Instance.cyberMonsterChannel.Stop();
    }
}
