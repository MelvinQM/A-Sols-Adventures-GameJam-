using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    [Header("Components")]
    [SerializeField] private Rigidbody2D rig;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [HideInInspector] public Vector2 moveInput;
    [HideInInspector] public bool isBeingMoved = false;


    void FixedUpdate()
    {
        if(!isBeingMoved){
            Vector2 velocity = moveInput * moveSpeed;
            rig.velocity = velocity;
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
