using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class HexTileManager : MonoBehaviour
{
    public static HexTileManager instance;
    public Dictionary<Vector3Int, HexTile> tiles;

    public GameObject highlight;
    public GameObject selector;
    public Vector3Int playerPos;

    public HexMovementController player;
    public List<HexTile> path;

    private void Awake()
    {
        instance = this;
        tiles = new Dictionary<Vector3Int, HexTile>();

        HexTile[] hexTiles = gameObject.GetComponentsInChildren<HexTile>();
        // Register all the tiles
        foreach(HexTile hexTile in hexTiles)
        {
            RegisterTile(hexTile);
        }
        // Get each tiles set of neighbors
        foreach(HexTile hexTile in hexTiles)
        {
            List<HexTile> neighbors = GetNeighbours(hexTile);
            hexTile.neighbors = neighbors;
        }


        // Put the player somewhere
        int randomTile = Random.Range(0, hexTiles.Length);
        HexTile tile = hexTiles[randomTile];
        while(tile.tileType != HexTileGenerationSettings.TileType.Standard)
        {
            tile = hexTiles[randomTile];
        }
        playerPos = tile.cubeCoordinate;
        player.transform.position = tile.transform.position + new Vector3(0, 1f, 0);
        player.currentTile = tile;
        
    }

    public void RegisterTile(HexTile tile)
    {
        tiles.Add(tile.cubeCoordinate, tile);
    }

    // Get the neighbors of each tile
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

    public void OnHighlightTile(HexHighlight tile)
    {
        highlight.transform.position = tile.transform.position;
    }

    public void OnSelectTile(HexHighlight tile)
    {
        selector.transform.position = tile.transform.position;
    }

    // Draw the path from the player to the selected tile
    public void OnDrawGizmos()
    {
        if(path != null)
        {
            foreach(HexTile tile in path)
            {
                Gizmos.DrawCube(tile.transform.position + new Vector3(0f, 0.5f, 0.5f), new Vector3(0.5f, 0.5f, 0.5f));
            }
        }        
    }
    
}