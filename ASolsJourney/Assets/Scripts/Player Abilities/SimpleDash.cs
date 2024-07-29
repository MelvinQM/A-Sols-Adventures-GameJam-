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
            // Dash function is called from player controller to avoid Coroutine issues
            _controller.StartDash(dashVelocity, activeTime);
        }
    }
}
