using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : Interactable
{
    [SerializeField] private GameObject door;
    private bool isDoorOpen = false;

    protected override void Interact()
    {
        isDoorOpen = !isDoorOpen;
        door.GetComponent<Animator>().SetBool("IsOpen", isDoorOpen);
        if (isDoorOpen)
        {
            promptMessage = "Press [E] : Open";
        }
        else
        {
            promptMessage = "Press [E] : Close";
        }
    }
}
