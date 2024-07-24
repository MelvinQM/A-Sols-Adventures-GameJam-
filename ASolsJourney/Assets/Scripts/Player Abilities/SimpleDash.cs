using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDash : Ability
{
    public float dashVelocity;

    // Cache storage for Components
    private PlayerController _controller;
    private Rigidbody2D _rb;

    public override void Activate(GameObject parent)
    {
        if (_controller == null)
        {
            _controller = parent.GetComponent<PlayerController>();
        }

        if (_rb == null)
        {
            _rb = parent.GetComponent<Rigidbody2D>();
        }

        if (_controller != null && _rb != null)
        {
            StartCoroutine(Dash(dashVelocity, activeTime));
        }
    }
    
    private IEnumerator Dash(float dashVelocity, float dashTime) {
        _controller.isBeingMoved = true;

        Vector2 dashDirection = _controller.moveInput.normalized;
        _rb.velocity = dashDirection * dashVelocity;
        yield return new WaitForSeconds(dashTime);

        _controller.isBeingMoved = false;
    }
}
