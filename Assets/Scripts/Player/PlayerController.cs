using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private bool isMoving;
    private float xRotation = 0f;
    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);

    [Header("Move and Jump")]
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float jumpHeight = 3.0f;

    [Header("Look")]
    [SerializeField] private Camera cam;
    [SerializeField] private float mouseSensitivity = 30.0f;
    [SerializeField] private float topClamp = -80.0f;
    [SerializeField] private float bottomClamp = 80.0f;

    [Header("Crouch")]
    [SerializeField] private bool isCrouching = false;
    [SerializeField] private float crouchTimer = 0f;
    [SerializeField] private bool isLerpCrouching = false;

    [Header("Sprint")]
    [SerializeField] private bool isSprinting = false;
    
    private void Start()
    {
        // Locking the cursor to the middle of the screen and making it invisible.
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Ground check
        isGrounded = controller.isGrounded;
        if (isLerpCrouching)
        {
            crouchTimer += Time.deltaTime;
            float t = crouchTimer / 1;
            t *= t;
            if (isCrouching)
                controller.height = Mathf.Lerp(controller.height, 1, t);
            else
                controller.height = Mathf.Lerp(controller.height, 2, t);
            if (t > 1) 
            {
                isLerpCrouching = false;
                crouchTimer = 0f;
            }
        }
        if ((lastPosition != gameObject.transform.position) && (isGrounded == true))
        {
            isMoving = true;
            // for later use.
        }
        else 
        {
            isMoving = false;
            // for later use.
        }
        lastPosition = gameObject.transform.position;
    }

    // Receive inputs from PlayerInputManager.cs and apply them to our character controller.
    public void PlayerMove(Vector2 input)
    {
        // Creating the moving vector
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;

        // Player movement in the x-z plane.
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        
        // Falling down.
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && (playerVelocity.y < 0f))
        {
            playerVelocity.y = -2.0f;
        }

        // Player movement along the y-axis.
        controller.Move(playerVelocity * Time.deltaTime);
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
        float mouseX = input.x * mouseSensitivity * Time.deltaTime;
        float mouseY = input.y * mouseSensitivity* Time.deltaTime;

        // Camera rotation arcund the x-axis (look up and down).
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);

        // Apply this to our camera's parent (eyes) transform.
        cam.transform.parent.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        // Rotate player to look left and right.
        transform.Rotate(Vector3.up * mouseX);
    }

    public void PlayerCrouch()
    {
        isCrouching = !isCrouching;
        crouchTimer = 0;
        isLerpCrouching = true;
    }

    public void PlayerSprint()
    {
        isSprinting = !isSprinting;
        if (isSprinting)
        {
            speed = 8.0f;
        }
        else
        {
            speed = 5.0f;
        }
    }
}
