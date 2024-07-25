using UnityEngine;

public abstract class ICollectible : MonoBehaviour
{
    public abstract void Collect();
    public abstract void OnTriggerEnter2D(Collider2D collision);
}
