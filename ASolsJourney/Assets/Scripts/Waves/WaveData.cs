using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WaveEnemyData
{
    public GameObject enemyPrefab;
    public Vector2 spawnPosition;
}

[CreateAssetMenu(fileName = "WaveData", menuName = "Wave Data", order = 1)]
public class WaveData : ScriptableObject
{
    public WaveEnemyData[] enemies;
    public bool isBossWave;
}
