using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerBottle : Interactable
{
    [SerializeField] private List<Rigidbody> allParts = new List<Rigidbody>();

    public void Shatter()
    {
        foreach (Rigidbody part in allParts)
        {
            part.isKinematic = false;
        }
    }
}
