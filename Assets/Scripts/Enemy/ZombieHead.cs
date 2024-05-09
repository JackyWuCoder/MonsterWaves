using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHead : MonoBehaviour
{
    [SerializeField] private List<Rigidbody> allParts = new List<Rigidbody>();
    [SerializeField] private GameObject headReplacement;
    [SerializeField] private GameObject headToDestroy;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (!collision.gameObject.CompareTag("Bullet"))
            return;
        GameObject.Instantiate(headReplacement, transform.position, transform.rotation);
        foreach (Rigidbody part in allParts)
        {
            part.isKinematic = false;
            part.AddExplosionForce(100.0f, transform.position, 5.0f);
        }
        Destroy(headToDestroy);
        animator.Play("Z_FallingBack");
    }
}
