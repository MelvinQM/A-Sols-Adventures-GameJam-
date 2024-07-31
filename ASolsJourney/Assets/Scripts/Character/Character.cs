using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Character : MonoBehaviour, IDamagable
{   
    public enum Team
    {
        Player,
        Enemy
    }
    [SerializeField] protected Team team;
    public enum LifeState
    {
        Alive,
        Death
    }

    protected LifeState lifeState = LifeState.Alive;

    public string DisplayName;
    public int CurHp;
    public int MaxHp;

    [Header("Audio")]
    [SerializeField] protected AudioSource audioSource;
    [SerializeField] protected AudioClip hitSFX;

    public event UnityAction onTakeDamage;
    public event UnityAction onHeal;
    public event Action<Character> OnDeath;


    // Virtual function to edit in children of this class
    public virtual void Die()
    {
        OnDeath?.Invoke(this);
    }

    public Team GetTeam()
    {
        return team;
    }

    public virtual void TakeDamage(int damageToTake)
    {
        if (lifeState == LifeState.Death) return;
        CurHp -= damageToTake;
        audioSource.PlayOneShot(hitSFX);

        onTakeDamage?.Invoke();

        if(CurHp <= 0) Die();
    }

    public virtual void Heal(int healAmount)
    {
        CurHp += healAmount;
        if (CurHp > MaxHp) CurHp = MaxHp;
        onHeal?.Invoke();
    }

    public abstract void Spawn();
}
