using Boss;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.EventSystems.EventTrigger;

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

        if (waves[currentWave - 1].isBossWave)
        {
            // Quickfix because the fireboss does not inherit from enemy and I want to sleep ;D
            FlameBoss boss = Instantiate(waves[currentWave - 1].enemies[0].enemyPrefab, waves[currentWave - 1].enemies[0].spawnPosition, Quaternion.identity, enemyContainer).GetComponent<FlameBoss>();
            boss.Spawn();

            boss.OnDeath += OnBossKilled;
        } else
        {
            foreach (WaveEnemyData enemyData in waves[currentWave - 1].enemies)
            {
                Enemy enemy = Instantiate(enemyData.enemyPrefab, enemyContainer).GetComponent<Enemy>();
                enemy.transform.position = enemyData.spawnPosition;
                enemy.Spawn();

                enemy.OnDeath += OnEnemyKilled;
            }
        }
        currentEnemyCount = waves[currentWave - 1].enemies.Length;
    }

    private void OnEnemyKilled(Character entity)
    {
        entity.OnDeath -= OnEnemyKilled;
        currentEnemyCount--;

        Debug.Log(currentEnemyCount);
        if (currentEnemyCount == 0) { EndWave(); }
    }

    private void OnBossKilled()
    {
        // DO SOMETHING
        Debug.Log("END OF GAME");
        SceneManager.LoadScene(3);
    }

    private void EndWave()
    {
        if (currentWave < waves.Length)
        {
            Debug.Log("Next Wave");
            StartWave(currentWave + 1);
        }
        else
        {
            Debug.Log("All waves completed!");
        }
    }
}
