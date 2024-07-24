using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleItem : MonoBehaviour, ICollectible
{
    public static event Action OnExampleItemPickedUp;
    public void Collect()
    {
        Debug.Log("Coin Collected");
        OnExampleItemPickedUp?.Invoke();
        Destroy(gameObject);
    }
}
