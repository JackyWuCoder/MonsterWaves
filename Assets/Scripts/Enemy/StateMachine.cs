using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected BaseState activeState;

    // Property for the patrol state.

    // Start is called before the first frame update
    protected void Start()
    {
        ChangeState(new PatrolState());
    }

    // Update is called once per frame
    protected void Update()
    {
        if (activeState != null)
        {
            activeState.Perform();
        }
    }

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

        // Check to make sure the new state wasn't null.
        if (activeState != null)
        {
            // Set up new state.
            activeState.SetStateMachine(this);
            activeState.SetEnemy(GetComponent<Enemy>());
            activeState.Enter();
        }
    }
}
