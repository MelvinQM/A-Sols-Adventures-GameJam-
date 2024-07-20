using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] GameObject enemyPrefab;

    [SerializeField] private float spawnInterval = 3.5f;
    [SerializeField] private float spawnRadiusMin = 5f;
    [SerializeField] private float spawnRadiusMax = 15f;

    private Transform spawnCenter;

    void Start()
    {
        StartCoroutine(spawnEnemy(spawnInterval, enemyPrefab));
        spawnCenter = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        float direction = Random.Range(0, Mathf.PI * 2);
        Vector3 spawnDirection = new Vector3(
            Mathf.Cos(direction),
            Mathf.Sin(direction)
            );
        float spawnDistance = Random.Range(spawnRadiusMin, spawnRadiusMax);
        Vector3 spawnLocation = spawnCenter.position + (spawnDirection * spawnDistance);
        GameObject newEnemy = Instantiate(enemy, spawnLocation, Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
