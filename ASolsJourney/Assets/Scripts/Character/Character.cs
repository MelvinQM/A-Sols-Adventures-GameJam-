using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Character : MonoBehaviour
{   
    public enum Team
    {
        Player,
        Enemy
    }
    [SerializeField] protected Team team;

    public string DisplayName;
    public int CurHp;
    public int MaxHp;

    [Header("Audio")]
    [SerializeField] protected AudioSource audioSource;
    [SerializeField] protected AudioClip hitSFX;

    public event UnityAction onTakeDamage;
    public event UnityAction onHeal;

    // Virtual function to edit in children of this class
    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public Team GetTeam()
    {
        return team;
    }

    public virtual void TakeDamage(int damageToTake)
    {
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
}
