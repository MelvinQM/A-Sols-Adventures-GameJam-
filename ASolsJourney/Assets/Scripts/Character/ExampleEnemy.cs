using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleEnemy : Enemy
{   
    [SerializeField] private int damage;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackRate;

    protected override void AttackTarget()
    {
        IDamagable damagable = target.GetComponent<IDamagable>();
        if(damagable != null)
            damagable.TakeDamage(damage);
    }

    protected override bool CanAttack()
    {
        return Time.time - lastAttackTime > attackRate;
    }

    protected override bool InAttackRange()
    {
        return targetDistance <= attackRange;
    }

    //NOTE: Probably remove the oncollision death mechanic later but leaving it in for now
    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.tag == "Player")
    //     {
    //         Debug.LogFormat("Hit Player: {0}!!!", collision.gameObject.name);
    //         Die();
    //     }
    // }
}
