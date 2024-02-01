using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    PlayerInputActions inputActions;
    Vector3 dir = Vector3.zero;
    Rigidbody rigid;
    public float moveSpeed = 5.0f;
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
        
        dir.x = context.ReadValue<Vector2>().x;
        switch (context.ReadValue<Vector2>().y)
        {
            case 0:
                dir.z = 0;
                break;
            case -1:
                dir.z = -1;
                break;
            case 1:
                dir.z = 1;
                break;
        }
    }


    private void Update()
    {
        rigid.MovePosition(rigid.position + Time.fixedDeltaTime * moveSpeed * dir);
    }
}
