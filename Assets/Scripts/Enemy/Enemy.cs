using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private const float MAXHEALTH = 100;
    protected float health = 100;
    [SerializeField] protected Slider healthBar;

    protected NavMeshAgent agent;
    [SerializeField] protected GameObject player;
    [SerializeField] protected Path path;
    private Vector3 lastSeenPlayerPos;
    
    [Header("Sight Values")]
    [SerializeField] protected float sightDistance = 20.0f;
    [SerializeField] protected float fieldOfView = 85.0f;
    // Allows for the enemy to see at eye level.
    [SerializeField] protected float eyeHeight;

    public NavMeshAgent Agent { get => agent; }
    public GameObject Player { get => player; }
    public Vector3 LastSeenPlayerPos { 
        get => lastSeenPlayerPos; set => lastSeenPlayerPos = value;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Mathf.Clamp(health, 0, MAXHEALTH);
        healthBar.value = health;
        CanSeePlayer();
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
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), playerDirection);
                    RaycastHit hitInfo = new RaycastHit();
                    if (Physics.Raycast(ray, out hitInfo, sightDistance))
                    {
                        if (hitInfo.transform.gameObject.CompareTag("Player"))
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

    public float GetHealth()
    {
        return health;
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
    }

}
