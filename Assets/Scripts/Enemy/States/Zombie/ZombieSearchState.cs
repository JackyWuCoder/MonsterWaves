using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSearchState : SearchState
{
    public override void Perform()
    {
        if (enemy.CanSeePlayer())
            stateMachine.ChangeState(new ZombieAttackState());
        if (enemy.Agent.remainingDistance < enemy.Agent.stoppingDistance)
        {
            searchTimer += Time.deltaTime;
            if (searchTimer > 10)
            {
                stateMachine.ChangeState(new ZombiePatrolState());
            }
        }
    }
}
