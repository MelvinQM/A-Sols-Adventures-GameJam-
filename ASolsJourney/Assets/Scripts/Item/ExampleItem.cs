using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleItem : ICollectible
{
    public static event Action OnExampleItemPickedUp;
    public override void Collect()
    {
        Debug.Log("Coin Collected");
        OnExampleItemPickedUp?.Invoke();
        Destroy(gameObject);
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        throw new NotImplementedException();
    }
}
