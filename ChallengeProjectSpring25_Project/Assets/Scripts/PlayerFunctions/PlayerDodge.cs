using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.Windows;
public class NewMonoBehaviourScript : MonoBehaviour
{
    private CharacterController controller;
    private PlayerInput input;
    private Vector2 move;
    private PlayerMovement playerMovement;

    [SerializeField] private float dodgeSpeed = 10f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (input.actions["Dodge"].triggered)
        {
            playerMovement.enabled = false;

            move = input.actions["Move"].ReadValue<Vector2>();
            Vector3 moveDirection = Vector3.zero;
            moveDirection = (transform.right * move.x + transform.forward * move.y).normalized;
            controller.Move(moveDirection * dodgeSpeed * Time.deltaTime);

            playerMovement.enabled = true;
        }
            

    }
}
