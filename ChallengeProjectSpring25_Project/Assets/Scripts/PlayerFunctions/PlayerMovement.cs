using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private bool useAcceleration = true;
    [SerializeField] private float accelerationMultiplier = 2f;
    [SerializeField] private float maxSpeedMultiplier = 1.5f;
    [SerializeField] private float deceleration = 15f;

    [Header("Jump & Gravity")]
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.81f;

    [Header("References")]
    [SerializeField] private PlayerLockOn playerLockOn;
    [SerializeField] private PlayerInput input;
    [SerializeField] private PlayerTether tether;

    private float acceleration;
    private float maxSpeed;
    private float currentSpeed = 0f;

    private CharacterController controller;
    private Vector2 move;
    private Vector3 playerVel;
    public bool grounded;

    public float coyoteTime;
    private float coyoteTimer;

    public float jumpStaminaCost;
    private PlayerStamina playerStam;
    public AudioClip jumpSound;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        playerStam = GetComponent<PlayerStamina>();

        acceleration = playerSpeed * accelerationMultiplier;
        maxSpeed = playerSpeed * maxSpeedMultiplier;
    }

    private void Update()
    {
        // Reset vertical velocity if grounded
        if (grounded && playerVel.y < 0)
        {
            playerVel.y = -2f;
        }

        move = input.actions["Move"].ReadValue<Vector2>();

        // Determine movement direction
        Vector3 moveDirection = Vector3.zero;
        if (!playerLockOn.lockedOn)
        {
            moveDirection = (transform.right * move.x + transform.forward * move.y).normalized;
        }
        else
        {
            Vector3 targetDir = (playerLockOn.target.position - transform.position).normalized;
            Vector3 rightDir = Vector3.Cross(Vector3.up, targetDir);
            Debug.DrawRay(transform.position, rightDir * 3f, Color.yellow);
            Debug.DrawRay(transform.position, -rightDir * 3f, Color.yellow);
            moveDirection = (rightDir * move.x + targetDir * move.y).normalized;
        }

        // Handle speed
        float speedToUse = playerSpeed;

        if (useAcceleration)
        {
            if (move.magnitude > 0.1f)
            {
                currentSpeed += acceleration * Time.deltaTime;
                currentSpeed = Mathf.Min(currentSpeed, maxSpeed);
            }
            else
            {
                currentSpeed -= deceleration * Time.deltaTime;
                currentSpeed = Mathf.Max(currentSpeed, 0f);
            }

            speedToUse = currentSpeed;
        }

        // Move the player
        Vector3 newPos = speedToUse * Time.deltaTime * moveDirection;
        if (tether.CanMoveTo(newPos)) controller.Move(newPos);

        // Coyote time logic
        if (grounded)
        {
            coyoteTimer = coyoteTime;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }

        // Jumping
        if ((grounded || coyoteTimer > 0) && input.actions["Jump"].triggered && playerStam.HasEnoughStamina(jumpStaminaCost))
        {
            GetComponent<AudioSource>().PlayOneShot(jumpSound);
            playerStam.ConsumeStam(jumpStaminaCost);
            playerVel.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        playerVel.y += gravity * Time.deltaTime;
        controller.Move(playerVel * Time.deltaTime);
    }

    public float GetSpeed()
    {
        return playerSpeed;
    }

    public void SetSpeed(float speed)
    {
        Debug.Log("Changed speed to: " + speed);
        playerSpeed = speed;

        acceleration = playerSpeed * accelerationMultiplier;
        maxSpeed = playerSpeed * maxSpeedMultiplier;
    }

    public float GetJump()
    {
        return jumpHeight;
    }

    public void SetJump(float height)
    {
        Debug.Log("Change jump height to: " + height);
        jumpHeight = height;
    }

    public void EnableAcceleration(bool enabled)
    {
        useAcceleration = enabled;
    }
}
