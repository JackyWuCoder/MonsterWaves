using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePatrolState : PatrolState
{
    public override void Perform()
    {
        base.Perform();
        if (enemy.CanSeePlayer())
        {
            stateMachine.ChangeState(new ZombieAttackState());
        }
    }
}
