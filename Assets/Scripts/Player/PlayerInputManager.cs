using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerController controller;

    private void Awake()
    {
        playerInput = new PlayerInput();
        controller = GetComponent<PlayerController>();
        // When player jump is performed, use a callback context to call the PlayerJump() method.
        playerInput.OnFoot.Jump.performed += ctx => controller.PlayerJump();
        playerInput.OnFoot.Crouch.performed += ctx => controller.PlayerCrouch(); // toggles
        playerInput.OnFoot.Sprint.performed += ctx => controller.PlayerSprint(); // toggles
    }

    private void FixedUpdate()
    {
        // Tell the player controller to move using the value from our "Move" action.
        controller.PlayerMove(playerInput.OnFoot.Move.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        // Tell the player controller to look using the value from our "Look" action.
        controller.PlayerLook(playerInput.OnFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        playerInput.OnFoot.Enable();
    }

    private void OnDisable()
    {
        playerInput.OnFoot.Disable();
    }

    public PlayerInput GetPlayerInput()
    {
        return playerInput;
    }
}
