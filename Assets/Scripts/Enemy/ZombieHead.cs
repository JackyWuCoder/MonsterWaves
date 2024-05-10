using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHead : MonoBehaviour
{
    [SerializeField] private List<Rigidbody> allParts = new List<Rigidbody>();
    [SerializeField] private GameObject headReplacement;
    [SerializeField] private GameObject headToDestroy;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Bullet"))
            return;
        gameObject.GetComponent<Enemy>().TakeDamage(30);
        if (gameObject.GetComponent<Enemy>().GetHealth() == 0)
        GameObject.Instantiate(headReplacement, transform.position, transform.rotation);
        foreach (Rigidbody part in allParts)
        {
            part.isKinematic = false;
            part.AddExplosionForce(100.0f, transform.position, 5.0f);
        }
        Destroy(headToDestroy);
    }
}
