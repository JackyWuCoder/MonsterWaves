using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    protected float moveTimer;
    protected float losePlayerTimer;

    public override void Enter()
    {
        
    }

    public override void Perform()
    {
        // Enemy can see the player.
        if (enemy.CanSeePlayer())
        {
            // Lock the lose player timer and increment the move timer.
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.Player.transform);
            if (moveTimer > Random.Range(3, 7))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));
                moveTimer = 0;
            }
            enemy.LastSeenPlayerPos = enemy.Player.transform.position;
        }
        else 
        {
            losePlayerTimer += Time.deltaTime;
        }
    }

    public override void Exit()
    {

    }
}
