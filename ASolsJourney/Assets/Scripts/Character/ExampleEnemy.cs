using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleEnemy : Enemy
{
    [SerializeField] private int damage;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackRate;
    [SerializeField] private GameObject dropItem;
    [SerializeField] private Transform sprite;
    [SerializeField] private Transform spawnSprite;

    protected override void AttackTarget()
    {
        IDamagable damagable = target.GetComponent<IDamagable>();
        if (damagable != null)
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

    public override void Die()
    {
        base.Die();
        Debug.Log("ExampleEnemy died");
        Instantiate(dropItem, transform.position, Quaternion.identity);
    }

    // public override void Spawn()
    // {
    //     Debug.Log("ANIMATION");

    //     // Turn off sprite of enemy
    //     sprite.gameObject.SetActive(false);

    //     // Turn on spawn sprite
    //     spawnSprite.gameObject.SetActive(true);

    //     // Play animation
    //     Animator ani = spawnSprite.GetComponent<Animator>();
    //     ani.Play("SpawningAnimation");

    //     sprite.gameObject.SetActive(true);
    //     spawnSprite.gameObject.SetActive(false);
    // }

    public override void Spawn()
    {
        StartCoroutine(PlaySpawnAnimation());
    }

    private IEnumerator PlaySpawnAnimation()
    {
        Debug.Log("ANIMATION");

        // Turn off sprite of enemy
        GameObject sprite = transform.Find("Sprite").gameObject;
        sprite.SetActive(false);

        // Turn on spawn sprite
        GameObject spawnSprite = transform.Find("SpawnSprite").gameObject;
        spawnSprite.SetActive(true);

        // Play animation
        Animator ani = spawnSprite.GetComponent<Animator>();
        ani.Play("SpawningAnimation");

        // Wait for the animation to complete
        yield return new WaitForSeconds(ani.GetCurrentAnimatorStateInfo(0).length);

        // Switch back to original sprite
        curState = State.Idle;
        sprite.SetActive(true);
        spawnSprite.SetActive(false);
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
