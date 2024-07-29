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
        Instantiate(dropItem, transform.position, Quaternion.identity);
        curState = State.Spawn;
        sprite.gameObject.SetActive(false);

        StartCoroutine(PlayDeathAnimation(() =>
        {
            base.Die();
        }));

        Debug.Log("ExampleEnemy died");
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
        // Turn off sprite of enemy
        sprite.gameObject.SetActive(false);

        // Turn on spawn sprite
        spawnSprite.gameObject.SetActive(true);

        // Play animation
        Animator ani = spawnSprite.GetComponent<Animator>();
        ani.Play("SpawningAnimation");

        // Wait for the animation to complete
        yield return new WaitForSeconds(ani.GetCurrentAnimatorStateInfo(0).length);

        // Switch back to original sprite
        sprite.gameObject.SetActive(true);
        spawnSprite.gameObject.SetActive(false);

        // Call the callback
        onComplete?.Invoke();
    }

    private IEnumerator PlayDeathAnimation(Action onComplete)
    {
        Debug.Log("ANIMATION");
        ParticleSystem particle = transform.GetComponent<ParticleSystem>();
        particle.Play();

        while (particle.isPlaying)
        {
            yield return null;
        }

        Debug.Log("AAA");

        onComplete?.Invoke();
    }
}
