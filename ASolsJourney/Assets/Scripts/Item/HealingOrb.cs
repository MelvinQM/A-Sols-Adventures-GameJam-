using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingOrb : ICollectible
{
    [SerializeField] int healAmount;

    public override void Collect()
    {
        Debug.Log("Collecting Healing Orb");
    }

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent<Player>(out var player))
        {
            Debug.Log("Is Character");
            player.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
