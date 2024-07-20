using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour, IDamagable
{
    public enum Team
    {
        Player,
        Enemy
    }

    public string DisplayName;
    public int CurHp;
    public int MaxHp;
    [SerializeField] private Team team;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hitSFX;
    public event UnityAction onTakeDamage;
    public event UnityAction onHeal;
    public void TakeDamage(int damageToTake)
    {
        CurHp -= damageToTake;
        audioSource.PlayOneShot(hitSFX);

        onTakeDamage?.Invoke();

        if(CurHp <= 0) Die();
    }
    public void Die() 
    {

    }

    public Team GetTeam()
    {
        return team;
    }

    public void Heal(int healAmount)
    {
        CurHp += healAmount;
        if (CurHp > MaxHp) CurHp = MaxHp;
        onHeal?.Invoke();
    }
}
