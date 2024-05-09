using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletDamage = 10;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(bulletDamage);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("hit " + collision.gameObject.name + " !");
        }
        if (collision.gameObject.CompareTag("Target"))
        { 
        }
        Destroy(gameObject);
    }
}
