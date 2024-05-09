using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : BaseState
{
    protected float searchTimer;

    public override void Enter()
    {
        enemy.Agent.SetDestination(enemy.LastSeenPlayerPos);
    }

    public override void Perform()
    {
        
    }

    public override void Exit()
    {
        
    }
}
