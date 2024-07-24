using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    void Start()
    {
        ExampleItem.OnExampleItemPickedUp += ExamplePickup;
    }

    void ExamplePickup()
    {
        Debug.Log("Player picked up example object");
    }
}
