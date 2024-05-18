using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Teleporter : Interactable
{
    [SerializeField] GameObject player;
    [SerializeField] Transform exitTeleporter;
    protected override void Interact()
    {
        TeleportPlayer();
    }

    private void TeleportPlayer()
    {
        // Teleport the player to the exit teleporter
        if (player != null)
        {
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = exitTeleporter.position;
            player.GetComponent<CharacterController>().enabled = true;
        }
    }
}
