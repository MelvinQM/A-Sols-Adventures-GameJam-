using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [Header("Components")]
    [SerializeField] private Rigidbody2D rig;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private MouseUtilities mouseUtils;

    private Vector2 moveInput;

    void Update()
    {
        Vector2 mouseDir = mouseUtils.GetMouseDirection(transform.position);
        spriteRenderer.flipX = mouseDir.x < 0;
    }

    void FixedUpdate()
    {
        Vector2 velocity = moveInput * moveSpeed;
        rig.velocity = velocity;
    }

    public void OnMoveInput (InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

}
