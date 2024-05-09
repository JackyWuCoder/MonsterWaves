using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberMonster : Enemy
{
    [Header("Weapon Values")]
    [SerializeField] private Transform gunBarrel;
    [Range(0.1f, 10.0f)]
    [SerializeField] private float fireRate;
}
