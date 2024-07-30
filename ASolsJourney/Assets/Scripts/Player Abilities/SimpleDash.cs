using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SimpleDash : Ability
{
    public float dashVelocity;

    // Cache storage for Components
    private PlayerController _controller;
    private Rigidbody2D _rb;


    public override void Activate(GameObject parent)
    {
        _controller.isBeingMoved = true;
        // Check if the _controller is null
        if (_controller == null)
        {
            _controller = parent.GetComponent<PlayerController>();
            Debug.Log("Controller was null, now assigned: " + _controller);
        }

        // Check if the _rb (Rigidbody2D) is null
        if (_rb == null)
        {
            _rb = parent.GetComponent<Rigidbody2D>();
            Debug.Log("Rigidbody2D was null, now assigned: " + _rb);
        }
        else
        {
            Debug.Log("Rigidbody2D already assigned: " + _rb);
        }

        // Check if moveInput is zero
        if (_controller.moveInput == Vector2.zero)
        {
            Debug.Log("MoveInput is zero. Exiting update.");
            return;
        }

        Vector2 dashDirection = _controller.moveInput.normalized;
        _rb.velocity = dashDirection * dashVelocity;
        _controller.dust.Play();
    }

    public override void BeginCoolDown(GameObject parent)
    {
        _controller.isBeingMoved = false;
    }

}
