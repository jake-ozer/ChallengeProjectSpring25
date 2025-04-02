using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private PlayerLockOn playerLockOn;
    [SerializeField] private PlayerInput input;
    
    [SerializeField] private PlayerTether tether;

    private CharacterController controller;
    private Vector2 move;
    private Vector3 playerVel;
    public bool grounded;

    public float coyoteTime;
    private float coyoteTimer;
    

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //ground check and saftey adjustment
        //grounded = controller.isGrounded;  ||| not using unity default one anymore, it is manually implemented to fix bug with scales arena
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

        Vector3 newPos = playerSpeed * Time.deltaTime * moveDirection;
        
        // check if the player can move
        if (tether.CanMoveTo(newPos)) controller.Move(newPos);

        //jump logic

        if (grounded)
        {
            coyoteTimer = coyoteTime;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }


        if ((grounded || coyoteTimer > 0) && input.actions["Jump"].triggered)
        {
            playerVel.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }


        


        //apply gravity
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
