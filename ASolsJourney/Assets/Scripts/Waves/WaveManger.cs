using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManger : MonoBehaviour
{

    [SerializeField] private WaveAnnouncer waveAnnouncer;
    [SerializeField] private WaveData[] waves;
    [SerializeField] private Transform enemyContainer;

    private int currentWave = 0;
    private int currentEnemyCount = 0;

    void Start()
    {
        StartWave(1);
    }

    void Update()
    {
        
    }

    private void StartWave(int waveNumber)
    {
        currentWave = waveNumber;
        waveAnnouncer.AnnounceWave(waveNumber);

        WaveAnnouncer.OnWaveAnnounceHidden += SpawnEnemies;
    }

    private void SpawnEnemies()
    {
        WaveAnnouncer.OnWaveAnnounceHidden -= SpawnEnemies;
        foreach (WaveEnemyData enemyData in waves[currentWave - 1].enemies)
        {
            Enemy enemy = Instantiate(enemyData.enemyPrefab, enemyContainer).GetComponent<Enemy>();
            enemy.transform.position = enemyData.spawnPosition;
        }
    }
}
