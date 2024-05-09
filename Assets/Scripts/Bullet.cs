using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletDamage = 10;

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
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("hit a wall");
        }
        Destroy(gameObject);
    }
}
