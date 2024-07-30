using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss
{
    [Serializable]
    public class EnemyAbility
    {
        [SerializeField] private float useChance;
        [SerializeField] private Ability ability;
    }
}