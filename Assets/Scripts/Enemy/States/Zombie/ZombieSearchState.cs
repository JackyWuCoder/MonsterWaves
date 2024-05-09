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
            moveTimer += Time.deltaTime;
            if(moveTimer > Random.Range(3, 5))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 10));
                moveTimer = 0;
            }
            if (searchTimer > 10)
            {
                stateMachine.ChangeState(new ZombiePatrolState());
            }
        }
    }
}
