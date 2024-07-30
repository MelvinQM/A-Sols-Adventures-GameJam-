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
        if (_controller == null) _controller = parent.GetComponent<PlayerController>();
        if (_rb == null) _rb = parent.GetComponent<Rigidbody2D>();
        if(_controller.moveInput == Vector2.zero) return;

        Vector2 dashDirection = _controller.moveInput.normalized;
        _rb.velocity = dashDirection * dashVelocity;
        _controller.dust.Play();
    }

    public override void BeginCoolDown(GameObject parent)
    {
        _controller.isBeingMoved = false;
    }

}
