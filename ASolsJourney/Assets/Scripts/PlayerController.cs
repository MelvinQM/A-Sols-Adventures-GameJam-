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
    [SerializeField] private Collider2D playerCollider;
    [HideInInspector] public Vector2 moveInput;
    [HideInInspector] public bool isBeingMoved = false;

    void FixedUpdate()
    {
        if (!isBeingMoved)
        {
            Vector2 velocity = moveInput * moveSpeed;
            rig.velocity = velocity;
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void PlayerDeath()
    {
        spriteRenderer.enabled = false;
        playerCollider.enabled = false;
        rig.simulated = false;
    }

    public void RevivePlayer()
    {
        spriteRenderer.enabled = true;
        playerCollider.enabled = true;
        rig.simulated = true;
        rig.position = new Vector2(0f, 0f);
    }

    private IEnumerator Dash(float dashVelocity, float dashTime)
    {
        isBeingMoved = true;

        Vector2 dashDirection = moveInput.normalized;
        rig.velocity = dashDirection * dashVelocity;
        yield return new WaitForSeconds(dashTime);

        isBeingMoved = false;
    }
    public void StartDash(float dashVelocity, float activeTime)
    {
        StartCoroutine(Dash(dashVelocity, activeTime));
    }
}
