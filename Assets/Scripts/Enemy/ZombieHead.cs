using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHead : MonoBehaviour
{
    [SerializeField] private GameObject headReplacement;
    [SerializeField] private GameObject headToDestroy;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (!collision.gameObject.CompareTag("Bullet"))
            return;
        GameObject.Instantiate(headReplacement, transform.position, transform.rotation);
        Destroy(headToDestroy);
    }
}
