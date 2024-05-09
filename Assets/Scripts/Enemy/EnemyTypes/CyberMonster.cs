using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CyberMonster : Enemy
{
    private CyberMonsterStateMachine stateMachine;

    [Header("Weapon Values")]
    [SerializeField] private Transform gunBarrel;
    [Range(0.1f, 10.0f)]
    [SerializeField] private float fireRate;

    // Only for debugging purposes.
    [Header("Debugging")]
    [SerializeField] protected string currentState;

    protected override void Start()
    {
        base.Start();
        stateMachine = GetComponent<CyberMonsterStateMachine>();
        stateMachine.Initialize();
    }

    protected override void Update()
    {
        base.Update();
        currentState = stateMachine.GetActiveState().ToString();
    }

    public float GetFireRate()
    {
        return fireRate;
    }

    public void Shoot()
    {
        Debug.Log("CyberMonster is Shooting.");
    }
}
