using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void TakeDamage(int damageToTake);
    void Die();
    Character.Team GetTeam();
}

