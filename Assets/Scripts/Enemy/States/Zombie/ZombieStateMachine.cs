using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStateMachine : StateMachine
{
    public override void ChangeState(BaseState newState)
    {
        base.ChangeState(newState);
        // Check to make sure the new state wasn't null.
        if (activeState != null)
        {
            // Set up new state.
            activeState.SetStateMachine(this);
            activeState.SetEnemy(GetComponent<Zombie>());
            activeState.Enter();
        }
    }

    public override void Start()
    {
        ChangeState(new ZombiePatrolState());
    }
}
