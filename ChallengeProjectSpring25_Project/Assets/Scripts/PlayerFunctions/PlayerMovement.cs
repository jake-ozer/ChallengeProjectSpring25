using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.81f;

    private CharacterController controller;
    private PlayerInput input;
    private Vector2 move;
    private Vector3 playerVel;
    private bool grounded;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        grounded = controller.isGrounded;
        if (grounded && playerVel.y < 0)
        {
            playerVel.y = -2f;
        }

        move = input.actions["Move"].ReadValue<Vector2>();
        Vector3 moveDirection = (transform.right * move.x + transform.forward * move.y).normalized;
        controller.Move(moveDirection * playerSpeed * Time.deltaTime);

        if (grounded && input.actions["Jump"].triggered)
        {
            playerVel.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        playerVel.y += gravity * Time.deltaTime;
        controller.Move(playerVel * Time.deltaTime);
    }
}
