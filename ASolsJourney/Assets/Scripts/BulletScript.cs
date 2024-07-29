using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int damage;
    [SerializeField] private int lifetime;

    void Start()
    {

    }

    public void Boom(float dirX, float dirY, float rot)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(dirX, dirY).normalized * bulletSpeed;
        
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);

        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
        if (damagable != null && damagable.GetTeam() == Character.Team.Enemy)
        {
            damagable.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
