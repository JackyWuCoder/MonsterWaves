using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberMonsterPatrolState : PatrolState
{
    public override void Perform()
    {
        base.Perform();
        if (enemy.CanSeePlayer())
        {
            stateMachine.ChangeState(new CyberMonsterAttackState());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
