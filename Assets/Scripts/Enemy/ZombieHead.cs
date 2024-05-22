using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHead : MonoBehaviour
{
    private GameObject rootParent;

    [SerializeField] private List<Rigidbody> allParts = new List<Rigidbody>();
    [SerializeField] private GameObject headReplacement;
    [SerializeField] private GameObject headToDestroy;

    private void Start()
    {
        rootParent = transform.root.gameObject;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Bullet"))
            return;
        GameObject zombie = GetRootParent(this.gameObject);
        zombie.GetComponent<Enemy>().TakeDamage(30);
        if (zombie.GetComponent<Enemy>().GetHealth() == 0)
        {
            GameObject.Instantiate(headReplacement, transform.position, transform.rotation);
            foreach (Rigidbody part in allParts)
            {
                part.isKinematic = false;
                part.AddExplosionForce(100.0f, transform.position, 5.0f);
            }
            Destroy(headToDestroy);
        }
    }

    public GameObject GetRootParent(GameObject child)
    {
        Transform currentParent = child.transform;
        // Traverse upwards in the hierarchy
        while (currentParent.parent != null)
        {
            currentParent = currentParent.parent;
        }
        return currentParent.gameObject;
    }
}
