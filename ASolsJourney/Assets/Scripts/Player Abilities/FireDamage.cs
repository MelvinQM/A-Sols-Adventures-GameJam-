using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour
{
    public int damage = 10;
    public float damageCooldown = 0.2f;

    private Dictionary<GameObject, float> lastDamageTime = new Dictionary<GameObject, float>();
    //Transform parentTransform;
    //public ParticleSystem fire;
    //private ParticleSystem.VelocityOverLifetimeModule velocityOverLifetime;
    // void Start()
    // {
    //     parentTransform = transform.parent;
    //     if (fire == null)
    //         fire = GetComponent<ParticleSystem>();
    //     velocityOverLifetime = fire.velocityOverLifetime; //TODO: Try to fix idk
    // }

    // void Update()
    // {
    //     // Aim the particle system at where the parent is looking
    //     AimParticleSystem();
    // }

    // private void AimParticleSystem()
    // {
    //     if (parentTransform != null && fire != null)
    //     {
    //         Vector3 direction = parentTransform.forward;
    //         // Set the velocity over lifetime in the direction the parent is facing
    //         velocityOverLifetime.x = new ParticleSystem.MinMaxCurve(direction.x);
    //         velocityOverLifetime.y = new ParticleSystem.MinMaxCurve(direction.y);
    //         velocityOverLifetime.z = new ParticleSystem.MinMaxCurve(direction.z);
    //     }
    // }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("FIRE HIT: " + other.name);
        IDamagable damagable = other.GetComponent<IDamagable>();
        if (damagable != null && damagable.GetTeam() == Character.Team.Enemy)
        {
            // Check if the enemy can be damaged (cooldown has expired)
            if (!lastDamageTime.ContainsKey(other) || Time.time >= lastDamageTime[other] + damageCooldown)
            {
                damagable.TakeDamage(damage);
                lastDamageTime[other] = Time.time; // Update the last damage time
            }
        }
    }
}
