using UnityEngine;

public class Personajes : MonoBehaviour
{
    // ================= MOVEMENT =================

    [Header("Movement")]

    // Character movement speed
    public float speed = 7f;

    // Character rotation smoothness
    public float rotationSpeed = 10f;

    // ================= JUMP =================

    [Header("Jump")]

    // Gravity force applied to character
    public float gravity = -20f;

    // Jump height
    public float jumpHeight = 2f;

    // ================= PLAYER =================

    [Header("Player")]

    // Player identifier (1 or 2)
    public int playerNumber = 1;

    // ================= CAMERA =================

    [Header("Camera")]

    // Camera reference for camera-relative movement
    public Transform cameraTransform;

    // CharacterController component
    private CharacterController controller;

    // Animator component
    private Animator animator;

    // Vertical velocity used for jumping and gravity
    private float verticalVelocity;

    void Start()
    {
        // Get CharacterController component
        controller = GetComponent<CharacterController>();

        // Get Animator component from children
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Horizontal and vertical input values
        float h = 0f;
        float v = 0f;

        // ================= INPUT =================

        // Player 1 controls
        if (playerNumber == 1)
        {
            if (Input.GetKey(KeyCode.A)) h = -1;
            if (Input.GetKey(KeyCode.D)) h = 1;
            if (Input.GetKey(KeyCode.W)) v = 1;
            if (Input.GetKey(KeyCode.S)) v = -1;

            // Jump input
            if (Input.GetKeyDown(KeyCode.Q) && controller.isGrounded)
            {
                Jump();
            }
        }
        // Player 2 controls
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow)) h = -1;
            if (Input.GetKey(KeyCode.RightArrow)) h = 1;
            if (Input.GetKey(KeyCode.UpArrow)) v = 1;
            if (Input.GetKey(KeyCode.DownArrow)) v = -1;

            // Jump input
            if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
            {
                Jump();
            }
        }

        // ================= CAMERA RELATIVE MOVEMENT =================

        // Get camera forward direction
        Vector3 forward = cameraTransform.forward;

        // Get camera right direction
        Vector3 right = cameraTransform.right;

        // Ignore vertical rotation
        forward.y = 0;
        right.y = 0;

        // Normalize vectors
        forward.Normalize();
        right.Normalize();

        // Calculate movement direction
        Vector3 moveDirection = forward * v + right * h;

        // Prevent faster diagonal movement
        if (moveDirection.magnitude > 1)
        {
            moveDirection.Normalize();
        }

        // ================= CHARACTER ROTATION =================

        // Rotate character toward movement direction
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        // ================= GRAVITY =================

        // Keep player grounded
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        // Apply gravity force
        verticalVelocity += gravity * Time.deltaTime;

        // ================= FINAL MOVEMENT =================

        // Horizontal movement
        Vector3 horizontalMove = moveDirection * speed;

        // Vertical movement
        Vector3 verticalMove = Vector3.up * verticalVelocity;

        // Combine horizontal and vertical movement
        Vector3 finalMove = horizontalMove + verticalMove;

        // Move character using CharacterController
        controller.Move(finalMove * Time.deltaTime);

        // ================= ANIMATIONS =================

        if (animator != null)
        {
            // Control walking animation
            animator.SetFloat("Speed", moveDirection.magnitude);

            // Control grounded animation state
            animator.SetBool("IsGrounded", controller.isGrounded);
        }
    }

    // Character jump function
    void Jump()
    {
        // Calculate jump force using physics formula
        verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
}