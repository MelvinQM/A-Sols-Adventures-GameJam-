using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour
{
    public int damage = 10;
    public float damageCooldown = 1;

    private Dictionary<GameObject, float> lastDamageTime = new Dictionary<GameObject, float>();
    private ParticleSystem fire;
    private ParticleSystem.VelocityOverLifetimeModule velocityOverLifetime;
    private readonly float velocityMultiplier = 2f; 
    private Camera mainCam;
    void Start()
    {
        if(mainCam == null)
            mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        if (fire == null )
            fire = GetComponent<ParticleSystem>();
            velocityOverLifetime = fire.velocityOverLifetime;

    }

    void Update()
    {
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = mousePos - transform.position;
        dir.Normalize();
        Vector3 rot = transform.position - mousePos;
        ShootInDirection(dir.x, dir.y, Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg);
        
    }

    void ShootInDirection(float dirX, float dirY, float rot) {
        //transform.rotation = Quaternion.Euler(0, 0, rot + 90);

        velocityOverLifetime.x = new ParticleSystem.MinMaxCurve(dirX * velocityMultiplier);
        velocityOverLifetime.y = new ParticleSystem.MinMaxCurve(dirY * velocityMultiplier);
    }

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
