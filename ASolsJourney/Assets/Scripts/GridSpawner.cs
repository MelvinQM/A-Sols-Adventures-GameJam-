using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridSpawner : MonoBehaviour
{

    [SerializeField] GameObject enemyPrefab;

    [SerializeField] private float spawnInterval = 3.5f;
    [SerializeField] private float spawnRadiusMin = 5f;
    [SerializeField] private Tilemap tilemap;
    // [SerializeField] private float spawnRadiusMax = 15f;

    private Transform spawnCenter;

    private List<Vector3> validSpawnPositions = new();

    void Start()
    {
        GetValidPositions();
        StartCoroutine(spawnEnemy(spawnInterval, enemyPrefab));
        spawnCenter = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Color color = new Color(1f, 1f, 0f);
        // Debug.DrawLine(new Vector3(3f, 3f), spawnCenter.position, color, 2.5f, false);
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GetValidPositions();
        StartCoroutine(spawnEnemy(interval, enemy));
    }

    private void GetValidPositions()
    {
        validSpawnPositions.Clear();
        BoundsInt boundsInt = tilemap.cellBounds;
        Vector3 tilesize = tilemap.layoutGrid.cellSize;
        TileBase[] allTiles = tilemap.GetTilesBlock(boundsInt);
        Vector3 start = tilemap.CellToWorld(new Vector3Int(boundsInt.xMin, boundsInt.yMin, 0));
        Debug.DrawLine(Vector3.zero, start, Color.red, 20f, false);

        for (int x = 0; x < boundsInt.size.x; x++)
        {
            for (int y = 0; y < boundsInt.size.y; y++)
            {
                TileBase tile = allTiles[x + y * boundsInt.size.x];
                if (tile != null)
                {
                    // Vector3 place = start + new Vector3(x, y);
                    Vector3 offset = new Vector3(
                        (x + 0.5f) * tilesize.x,
                        (y + 0.5f) * tilesize.y
                        );
                    Vector3 place = start + offset;

                    validSpawnPositions.Add(place);

                    Debug.DrawLine(Vector3.zero, place, Color.blue, 20f, false);
                }
            }
        }
    }
}
