using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHand : MonoBehaviour
{
    [SerializeField] private int damage;

    public int GetDamage() {
        return damage;
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

}
