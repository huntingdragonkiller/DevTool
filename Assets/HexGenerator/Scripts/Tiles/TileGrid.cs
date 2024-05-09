using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
    Dictionary<Vector3Int, Tile> tileDict = new Dictionary<Vector3Int, Tile>();
    Dictionary<Vector3Int, List<Vector3Int>> tileNeighborsDict = new Dictionary<Vector3Int, List<Vector3Int>>();

    private void Start()
    {
        foreach(Tile tile in FindObjectsOfType<Tile>())
        {
            tileDict[tile.tileCoords] = tile;
        }
    }

    public Tile GetTileAt(Vector3Int tileCoordinates)
    {
        Tile result = null;
        tileDict.TryGetValue(tileCoordinates, out result);
        return result;
    }

    public List<Vector3Int> GetNeighboursFor(Vector3Int coordinates)
    {
        
        if (tileDict.ContainsKey(coordinates) == false)
            return new List<Vector3Int>();
        
        if (tileNeighborsDict.ContainsKey(coordinates))
            return tileNeighborsDict[coordinates];
        
        tileNeighborsDict.Add(coordinates, new List<Vector3Int>());
        
        foreach (Vector3Int direction in Direction.GetDirectionList(coordinates.z))
        {
            if (tileDict.ContainsKey(coordinates + direction))
            {
                tileNeighborsDict[coordinates].Add(coordinates + direction);
            }
        }

        return tileNeighborsDict[coordinates];
    }

    internal Vector3Int GetClosestHex(Vector3 worldPosition)
    {
        worldPosition.y = 0;
        return TileCoordinates.ConvertPositionToOffset(worldPosition);
    }
}

public static class Direction
{
    public static List<Vector3Int> directionsOffsetOdd = new List<Vector3Int>
    {
        new Vector3Int(-1, 0, 1), // N1
        new Vector3Int(0, 0, 1), // N2
        new Vector3Int(1, 0, 0), // E
        new Vector3Int(0, 0, -1), // S2
        new Vector3Int(-1, 0, -1), // S1
        new Vector3Int(-1, 0, 0), // W
    };

    public static List<Vector3Int> directionsOffsetEven = new List<Vector3Int>
    {
        new Vector3Int(0, 0, 1), // N1
        new Vector3Int(1, 0, 1), // N2
        new Vector3Int(1, 0, 0), // E
        new Vector3Int(1, 0, -1), // S2
        new Vector3Int(0, 0, -1), // S1
        new Vector3Int(-1, 0, 0), // W
    };

    public static List<Vector3Int> GetDirectionList(int z)
        => z % 2 == 0 ? directionsOffsetEven : directionsOffsetOdd;
}
