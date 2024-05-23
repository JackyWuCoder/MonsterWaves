using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackState : AttackState
{
    private Zombie zombieEnemy;

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
