using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private PlayerController playerController;
    void Start()
    {
        healthBar.SetMaxHealth(MaxHp);
        healthBar.SetHealth(CurHp);

        ExampleItem.OnExampleItemPickedUp += ExamplePickup;
    }

    void ExamplePickup()
    {
        Debug.Log("Player picked up example object");
    }

    void Update()
    {
        healthBar.SetHealth(CurHp);
    }

    public override void Die()
    {
        Debug.Log("Player died");
        playerController.PlayerDeath();
        //base.Die(); Deleting player = bad idea
    }
}
