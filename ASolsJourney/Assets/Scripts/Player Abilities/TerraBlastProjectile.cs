using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class TerraBlastProjectile : MonoBehaviour
{
    public float rotationsPerMinute = 100.0f;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int damage;
    [SerializeField] private int lifetime;
    Animator ani;

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
            StartCoroutine(PlaySpawnAnimation(() =>
            {
                Destroy(gameObject);
            }));
            
        }
    }

    private IEnumerator PlaySpawnAnimation(Action onComplete)
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Animator ani = GetComponent<Animator>();
        ani.Play("TerraBlast");
        yield return new WaitForSeconds(ani.GetCurrentAnimatorStateInfo(0).length);
        onComplete?.Invoke();
    }
    void Update()
    {
        transform.Rotate(0,0,rotationsPerMinute * Time.deltaTime);
    }

}
