using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float bulletDamage = 10;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject objectWeHit = collision.gameObject;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            CreateBulletImpactEffect(collision);
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Environment"))
        {
            CreateBulletImpactEffect(collision);
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Target"))
        {
            CreateBulletImpactEffect(collision);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Beer"))
        {
            Debug.Log("hit a beer bottle");
            objectWeHit.gameObject.GetComponent<BeerBottle>().Shatter();
            // We will not destroy the bullet on impact, it will get destroyed according to its lifetime
        }
        CreateBulletImpactEffect(collision);
        Destroy(gameObject);
    }

    private void CreateBulletImpactEffect(Collision objectWeHit)
    {
        ContactPoint contact = objectWeHit.contacts[0];
        GameObject hole = Instantiate(
            GlobalReferences.Instance.bulletImpactEffectPrefab,
            contact.point,
            Quaternion.LookRotation(contact.normal)
            );
        hole.transform.SetParent(objectWeHit.gameObject.transform);
    }
}
