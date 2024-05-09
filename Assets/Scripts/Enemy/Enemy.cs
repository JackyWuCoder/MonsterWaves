using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    protected StateMachine stateMachine;
    protected NavMeshAgent agent;
    protected GameObject player;

    // Only for debugging purposes.
    [SerializeField] protected string currentState;
    [SerializeField] protected Path path;

    [Header("Sight Values")]
    [SerializeField] protected float sightDistance = 20.0f;
    [SerializeField] protected float fieldOfView = 85.0f;
    // Allows for the enemy to see at eye level.
    [SerializeField] protected float eyeHeight;

    [Header("Weapon Values")]
    [SerializeField] protected Transform BulletSpawnPoint;

    public NavMeshAgent Agent { get => agent; }

    // Start is called before the first frame update
    protected void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialize();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    protected void Update()
    {
        CanSeePlayer();
        currentState = stateMachine.GetActiveState().ToString();
    }

    public Path GetPath()
    {
        return path;
    }

    public bool CanSeePlayer()
    {
        if (player != null)
        {
            // Is the player close enough to be seen?
            if (Vector3.Distance(transform.position, player.transform.position) < sightDistance)
            {
                Vector3 playerDirection = player.transform.position - transform.position - (Vector3.up * eyeHeight);
                float angleToPlayer = Vector3.Angle(playerDirection, transform.forward);
                if((angleToPlayer >= -fieldOfView) && (angleToPlayer <= fieldOfView))
                {
                    Debug.Log("Sees Player!");
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), playerDirection);
                    RaycastHit hitInfo = new RaycastHit();
                    if (Physics.Raycast(ray, out hitInfo, sightDistance))
                    {
                        if (hitInfo.transform.gameObject != player)
                        {
                            return true;
                        }

                    }
                    Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                }
            }
        }
        return false;
    }
}
