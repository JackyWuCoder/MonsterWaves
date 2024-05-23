using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberMonsterAttackState : AttackState
{
    private CyberMonster cyberEnemy;
    private float shotTimer;

    public float stopAttackingDistance = 10f;

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
        LookAtPlayer();
        float distanceFromPlayer = Vector3.Distance(enemy.player.transform.position, enemy.animator.transform.position);
        // Enemy can see the player.
        if (enemy.CanSeePlayer() || distanceFromPlayer < stopAttackingDistance)
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
                enemy.animator.SetBool("isAttacking", false);
                stateMachine.ChangeState(new CyberMonsterChaseState());
            }
        }
    }

    private void LookAtPlayer()
    {
        Vector3 direction = enemy.player.transform.position - enemy.agent.transform.position;
        enemy.agent.transform.rotation = Quaternion.LookRotation(direction);

        var yRotation = enemy.agent.transform.eulerAngles.y;
        enemy.agent.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public override void Exit()
    {
        SoundManager.Instance.cyberMonsterChannel.Stop();
    }
}
