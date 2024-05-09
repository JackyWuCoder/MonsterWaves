using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float bulletDamage = 10;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(bulletDamage);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            
        }
        Destroy(gameObject);
    }
}
