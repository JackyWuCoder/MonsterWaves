using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberMonsterAttackState : AttackState
{
    private CyberMonster cyberEnemy;
    private float shotTimer;

    public override void Enter()
    {
        cyberEnemy = (CyberMonster)enemy;
    }

    public override void Perform()
    {
        base.Perform();
        // Enemy can see the player.
        if (enemy.CanSeePlayer())
        {
            // Increment the shotTimer;
            shotTimer += Time.deltaTime;
            if (shotTimer > cyberEnemy.GetFireRate())
            {
                cyberEnemy.Shoot();
                shotTimer = 0;
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