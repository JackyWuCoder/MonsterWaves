using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberMonsterStateMachine : StateMachine
{
    public override void Awake()
    {
        ChangeState(new CyberMonsterIdleState());
    }

    public override void Update()
    {
        if (activeState != null)
        {
            activeState.Perform();
        }
    }

    public override void ChangeState(BaseState newState)
    {
        base.ChangeState(newState);
        // Check to make sure the new state wasn't null.
        if (activeState != null)
        {
            // Set up new state.
            activeState.SetStateMachine(this);
            activeState.SetEnemy(GetComponent<CyberMonster>());
            activeState.Enter();
        }
    }
}
