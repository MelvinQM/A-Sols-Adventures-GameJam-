using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : Singleton<TileManager>
{
    public Tilemap tilemapCollision;
    public Tilemap tilemapNoCollision;
    public float minDistanceFromCollision = 1.0f;

    private BoundsInt tilemapNoCollisionBounds;
    private List<Vector3> collisionTilePositions;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the bounds and positions
        tilemapNoCollisionBounds = tilemapNoCollision.cellBounds;
        collisionTilePositions = GetCollisionTilePositions();
    }

    public Vector3 GetRandomPos()
    {
        Vector3 randomPos = Vector3.zero;
        bool validPosition = false;

        while (!validPosition)
        {
            Vector3Int randomCellPosition = new Vector3Int(
                Random.Range(tilemapNoCollisionBounds.xMin, tilemapNoCollisionBounds.xMax),
                Random.Range(tilemapNoCollisionBounds.yMin, tilemapNoCollisionBounds.yMax),
                0
            );

            randomPos = tilemapNoCollision.CellToWorld(randomCellPosition) + tilemapNoCollision.cellSize / 2;

            if (IsPositionValid(randomPos))
            {
                validPosition = true;
            }
        }

        return randomPos;
    }

    private List<Vector3> GetCollisionTilePositions()
    {
        List<Vector3> positions = new List<Vector3>();
        foreach (var pos in tilemapCollision.cellBounds.allPositionsWithin)
        {
            if (tilemapCollision.HasTile(pos))
            {
                Vector3 worldPos = tilemapCollision.CellToWorld(pos) + tilemapCollision.cellSize / 2;
                positions.Add(worldPos);
            }
        }
        return positions;
    }

    private bool IsPositionValid(Vector3 position)
    {
        foreach (var colPos in collisionTilePositions)
        {
            if (Vector3.Distance(position, colPos) < minDistanceFromCollision)
            {
                return false;
            }
        }
        return true;
    }
}
