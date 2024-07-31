using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerManager : Singleton<DangerManager>
{
    [SerializeField] private DangerCircle dangerPrefab;
    [SerializeField] private Transform parent;

    public void SpawnDangerZone(float width, Vector2 position, float lifeTime)
    {
        DangerCircle danger = Instantiate(dangerPrefab, parent);
        danger.Setup(width, position, lifeTime);
        danger.ShowDanger();
    }
}
