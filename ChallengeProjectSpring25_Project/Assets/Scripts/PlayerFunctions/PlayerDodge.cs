using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.Windows;
public class PlayerDodge : MonoBehaviour
{
    private CharacterController controller;
    private Vector2 move;
    private PlayerMovement playerMovement;
    private float elapsedTime;
    private bool isDodging = false;
    private Vector3 moveDirection;
    private Vector3 startPosition;
    private Vector3 endPosition;
    [SerializeField] private PlayerInput input;
    [SerializeField] private AnimationCurve interpCurve;
    [SerializeField] private float dodgeRange = 10f;
    [SerializeField] private float dodgeDuration = 1f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerMovement = GetComponent<PlayerMovement>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (input.actions["Sprint"].triggered && !isDodging && playerMovement.grounded)
        {
            //Debug.Log("Dodging");
            playerMovement.enabled = false;
            isDodging = true;
            move = input.actions["Move"].ReadValue<Vector2>();
            if (move == Vector2.zero)
            {
                move = Vector2.down;
            }
            moveDirection = Vector3.zero;
            moveDirection = (transform.right * move.x + transform.forward * move.y).normalized;
            startPosition = controller.transform.position;

            endPosition = startPosition + (moveDirection * dodgeRange);
            elapsedTime = 0;
        }

        //apply gravity
        //playerVel.y += gravity * Time.deltaTime;
        //controller.Move(playerVel * Time.deltaTime);

        if (isDodging)
        {
            elapsedTime += Time.deltaTime;
            float percentComplete = elapsedTime / dodgeDuration;

            controller.Move(Vector3.Lerp(startPosition, endPosition, interpCurve.Evaluate(percentComplete)) - controller.transform.position);

            if (percentComplete >= 1)
            {
                playerMovement.enabled = true;
                isDodging = false;
            }

        }


        
    }
}
