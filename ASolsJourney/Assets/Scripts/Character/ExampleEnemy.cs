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

    public override void Spawn()
    {
        StartCoroutine(PlaySpawnAnimation(() =>
        {
            base.Spawn();
        }));
    }

    private IEnumerator PlaySpawnAnimation(Action onComplete)
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
        sprite.SetActive(true);
        spawnSprite.SetActive(false);

        // Call the callback
        onComplete?.Invoke();
    }
}
