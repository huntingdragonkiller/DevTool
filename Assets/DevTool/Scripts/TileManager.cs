using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager instance;
    //public static HexTile tiles;
    /*
    private void Awake()
    {
        instance = this;
        //tiles = new Dictionary<Vector3Int, HexTile>();

        HexTile[] hexTiles = gameObject.GetComponentsInChildren<HexTile>();
        // Register all the tiles
        foreach(HexTile hexTile in hexTiles)
        {
            RegisterTile(hexTile);
        }
        // Get each tiles set of neighbors
        foreach(HexTile hexTile in hexTiles)
        {
            List<HexTile> neighbor = GetNeighbours(hexTile);
        }
    }
    
    public void RegisterTile(HexTile tile)
    {
        //tiles.Add(tile.cubeCoordinate, tile);
    }

    private List<HexTile> GetNeighbours(HexTile tile)
    {
        List<HexTile> neighbors = new List<HexTile>();

        Vector3Int[] neighborCoords = new Vector3Int[]
        {
            new Vector3Int(1, -1, 0),
            new Vector3Int(1, 0, -1),
            new Vector3Int(0, 1, -1),
            new Vector3Int(-1, 1, 0),
            new Vector3Int(-1, 0, 1),
            new Vector3Int(0, -1, 1),
        };

        foreach(Vector3Int neighborCoord in neighborCoords)
        {
            Vector3Int tileCoord = tile.cubeCoordinate;

            if(tiles.TryGetValue(tileCoord + neighborCoord, out HexTile neighbor))
            {
                neighbors.Add(neighbor);
            }
        }

        return neighbors;
    }

    public void OnDrawGizmosSelected()
    {
        foreach (HexTile neighbor in neighbors)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, 0.1f);
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, neighbor.transform.position);
        }
    }
    */
}
