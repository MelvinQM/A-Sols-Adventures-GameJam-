using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class TerraBlastProjectile : BulletScript
{
    public float spinCooldown = 0f;
    public float timeBetween = 0.1f;
    public float rotationSpeed = 10f;
    void Update()
    {
        if (spinCooldown < timeBetween)
        {
            spinCooldown += Time.deltaTime;
        }
        else
        {
            spinCooldown = 0f;
            transform.Rotate(0f, 0f, rotationSpeed);
        }
    }
}
