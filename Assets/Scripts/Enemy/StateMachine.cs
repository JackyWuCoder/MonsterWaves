using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected BaseState activeState;

    // Property for the patrol state.

    // Start is called before the first frame update
    public abstract void Awake();

    // Update is called once per frame
    public abstract void Update();

    public void Initialize()
    {
       // Set up default state.
    }

    public BaseState GetActiveState()
    {
        return activeState;
    }

    public virtual void ChangeState(BaseState newState)
    {
        // Check if there is an active state.
        if (activeState != null)
        {
            // Run cleanup on active state.
            activeState.Exit();
        }
        // Change to a new state.
        activeState = newState;
    }
}
