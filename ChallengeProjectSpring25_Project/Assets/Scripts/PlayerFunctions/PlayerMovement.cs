using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] [Range(0,1)] private float jumpCutMultiplier = 0.25f;
    [SerializeField] private float jumpHangThreshold = 1f;
    [SerializeField] private float jumpHangMultiplier = 0.6f;
    [SerializeField] private PlayerLockOn playerLockOn;
    [SerializeField] private PlayerInput input;

    private CharacterController controller;
    private Vector2 move;
    [SerializeField]
    private Vector3 playerVel;
    public bool grounded;

    private bool jumpInputHeld;
    private bool isJumping;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        jumpInputHeld = false;
        isJumping = false;
    }

    private void Update()
    {
        //ground check and saftey adjustment
        grounded = controller.isGrounded;
        if(grounded)
        {
            isJumping = false;
        }

        if (grounded && playerVel.y < 0)
        {
            playerVel.y = -2f;
        }

        //move logic
        move = input.actions["Move"].ReadValue<Vector2>();
        //if not locked on, move normally, if locked on, move perpinduclar to target
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

        //check if jump is held
        jumpInputHeld = Input.GetKey("space");

        controller.Move(moveDirection * playerSpeed * Time.deltaTime);

        //jump logic
        if (grounded && input.actions["Jump"].triggered)
        {
            playerVel.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isJumping = true;
        }

        //jump cut logic
        if(playerVel.y > 0 && isJumping && !jumpInputHeld)
        {
            playerVel.y *= (1 - jumpCutMultiplier);
        }

        //jump hang logic
        float usedGravity = gravity;
        if(Mathf.Abs(playerVel.y) <= jumpHangThreshold && isJumping)
        {
            usedGravity *= jumpHangMultiplier;
        }

        //apply gravity
        playerVel.y += usedGravity * Time.deltaTime;
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

}
