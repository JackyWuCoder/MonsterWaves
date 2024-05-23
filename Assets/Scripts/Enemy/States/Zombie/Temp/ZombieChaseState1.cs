using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieChaseState1 : StateMachineBehaviour
{
    [SerializeField] private float chaseSpeed = 6f;
    [SerializeField] private float stopChasingDistance = 21f;
    [SerializeField] private float attackingDistance = 2.5f;

    private Transform player;
    private NavMeshAgent agent;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Initialization
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();

        agent.speed = chaseSpeed;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (SoundManager.Instance.cyberMonsterChannel.isPlaying == false)
        {
            SoundManager.Instance.cyberMonsterChannel.PlayOneShot(SoundManager.Instance.zombieChase);
        }
        agent.SetDestination(player.position);
        animator.transform.LookAt(player);

        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);

        // Check if agent should stop chasing
        if (distanceFromPlayer > stopChasingDistance)
        {
            animator.SetBool("isChasing", false);
        }

        // Check if agent should attack
        if (distanceFromPlayer < attackingDistance)
        {
            animator.SetBool("isAttacking", true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Stops the agent from moving
        agent.SetDestination(animator.transform.position);
        SoundManager.Instance.cyberMonsterChannel.Stop();
    }
}
