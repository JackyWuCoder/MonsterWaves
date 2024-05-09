using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private BaseState activeState;

    // Property for the patrol state.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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

    public void ChangeState(BaseState newState)
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
            // Assign state enemy class.
            activeState.Enter();
        }
    }
}
