using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : BaseState
{
    public override void Enter()
    {

    }

    public override void Perform()
    {
        if (enemy.GetHealth() == 0)
        {
            enemy.Die();
        }
    }

    public override void Exit()
    {

    }
}
