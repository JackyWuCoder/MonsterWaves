using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    // Track which waypoint we are currently targeting.
    private int waypointIndex;

    public override void Enter()
    {
       
    }

    public override void Perform()
    {
        PatrolCycle();
    }

    public override void Exit()
    {
        
    }

    public void PatrolCycle()
    {
        // Implement patrol logic.
        if (enemy.Agent.remainingDistance < 0.2f)
        {
            List<Transform> wayPoints = enemy.GetPath().GetWaypoints();
            if (waypointIndex < wayPoints.Count - 1)
                waypointIndex++;
            else
                waypointIndex = 0;
            enemy.Agent.SetDestination(wayPoints[waypointIndex].position);
        }
    }
}