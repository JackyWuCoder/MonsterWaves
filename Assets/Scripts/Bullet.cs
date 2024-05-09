using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletDamage = 10;
    private void OnCollisionEnter(Collision collision)
    {
        Transform target = collision.transform;
        if (target.CompareTag("Player"))
        {
            target.GetComponent<PlayerHealth>().TakeDamage(bulletDamage);
        }
        if (target.CompareTag("Enemy"))
        {
        }
        Debug.Log("hit " + collision.gameObject.name + " !");
        Destroy(gameObject);
    }
}
