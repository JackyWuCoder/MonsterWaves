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
    }

    private void FixedUpdate()
    {
        // Tell the player controller to move using the value from our movement action.
        controller.PlayerMove(playerInput.OnFoot.Movement.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        playerInput.OnFoot.Enable();
    }

    private void OnDisable()
    {
        playerInput.OnFoot.Disable();
    }
}
