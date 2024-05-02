using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private float xRotation = 0f;

    [Header("Move and Jump")]
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float jumpHeight = 3.0f;

    [Header("Look")]
    [SerializeField] private Camera cam;
    [SerializeField] private float xSensitivity = 30.0f;
    [SerializeField] private float ySensitivity = 30.0f;
    
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
    }

    // Receive inputs from PlayerInputManager.cs and apply them to our character controller.
    public void PlayerMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        // Player movement in the x-z plane.
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && (playerVelocity.y < 0f))
        {
            playerVelocity.y = -2.0f;
        }
        // Player movement along the y-axis.
        controller.Move(playerVelocity * Time.deltaTime);
        Debug.Log(playerVelocity.y);
    }

    public void PlayerJump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void PlayerLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        // Calculate camera rotation for looking up and down.
        xRotation -= mouseY * ySensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -80.0f, 80.0f);
        // Apply this to our camera transform.
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        // Rotate player to look left and right.
        transform.Rotate(Vector3.up * mouseX * xSensitivity * Time.deltaTime);
    }
}
