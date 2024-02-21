using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    PlayerInputActions inputActions;
    Vector3 dir = Vector3.zero;
    Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        inputActions = new();
    }
    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
    }


    private void OnDisable()
    {
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (input != null)
        {
            moveDirection = new Vector3(input.x, 0f, input.y);
            Debug.Log($"SEND_MESSAGE : {input.magnitude}");
        }
    }

    private Vector3 moveDirection;
    private float moveSpeed = 4f;


    void Update()
    {
        bool hasControl = (moveDirection != Vector3.zero);
        if (hasControl)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }

}
