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
    [SerializeField] private ParticleSystem dust;
    [HideInInspector] public Vector2 moveInput;
    [HideInInspector] public bool isBeingMoved = false;
    public bool isDashing = false;
    public bool isWalking = false;

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
    }

    private IEnumerator Dash(float dashVelocity, float dashTime) {
        isBeingMoved = true;
        
        Vector2 dashDirection = moveInput.normalized;
        rig.velocity = dashDirection * dashVelocity;
        yield return new WaitForSeconds(dashTime);

        isBeingMoved = false;
    }
    public void StartDash(float dashVelocity, float activeTime) 
    {
        // If player is stationary dont dash
        if(moveInput == Vector2.zero) return;
        
        Debug.Log("Dash");
        dust.Play();
        StartCoroutine(Dash(dashVelocity, activeTime));
    }

}
