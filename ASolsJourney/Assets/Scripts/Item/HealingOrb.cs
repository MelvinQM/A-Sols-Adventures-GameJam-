using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingOrb : ICollectible
{

    public override void Collect()
    {
        Debug.Log("Collecting Healing Orb");
    }

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent<Character>(out var damagable))
        {
            Debug.Log("Is Character");
        }
    }
}
