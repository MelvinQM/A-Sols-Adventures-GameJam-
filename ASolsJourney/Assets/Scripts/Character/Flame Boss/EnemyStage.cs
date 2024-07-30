using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss
{
    [Serializable]
    public class EnemyStage
    {
        [SerializeField] private EnemyAbility[] abilities;
        [SerializeField] private float maxHealth;
    }
}