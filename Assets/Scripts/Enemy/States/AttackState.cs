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

    public override void Exit()
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
            if (moveTimer > Random.Range(3, 7))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));
                moveTimer = 0;
            }
        }
        else 
        {
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer > 8)
            {
                // Change to the search state
                stateMachine.ChangeState(new PatrolState());
            }
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
