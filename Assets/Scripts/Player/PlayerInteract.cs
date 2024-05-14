using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float rayDistance = 3.0f;
    [SerializeField] private LayerMask interactableMask;
    private PlayerUI playerUI;
    private PlayerInputManager playerInputManager;

    private void Start()
    {
        playerUI = GetComponent<PlayerUI>();
        playerInputManager = GetComponent<PlayerInputManager>();
    }

    private void Update()
    {
        playerUI.UpdateText(string.Empty);
        // Creates a ray at the center of the camera, shooting outwards.
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * rayDistance);
        RaycastHit hitInfo; // Variable that stores the collision information
        if (Physics.Raycast(ray, out hitInfo, rayDistance, interactableMask))
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.GetPromptMessage());
                PlayerInput playerInput = playerInputManager.GetPlayerInput();
                if (playerInput.OnFoot.Interact.triggered)
                {
                    interactable.BaseInteract();
                }
            }
        }
    }
}
