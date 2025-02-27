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
    [SerializeField] private PlayerInput input;
    

    [SerializeField] private float dodgeSpeed = 25f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerMovement = GetComponent<PlayerMovement>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (input.actions["Sprint"].triggered)
        {
            Debug.Log("Dodging");
            playerMovement.enabled = false;
            
            move = input.actions["Move"].ReadValue<Vector2>();
            Vector3 moveDirection = Vector3.zero;
            moveDirection = (transform.right * move.x + transform.forward * move.y).normalized;
            controller.Move(moveDirection * dodgeSpeed * Time.deltaTime);

            playerMovement.enabled = true;
        }
            

    }
}
