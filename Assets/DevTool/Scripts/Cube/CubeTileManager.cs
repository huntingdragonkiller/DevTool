using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTileManager : MonoBehaviour
{
    public static CubeTileManager instance;
    public Dictionary<Vector3Int, CubeTile> tiles;
    Dictionary<Vector3Int, List<Vector3Int>> tileNeighborDict = new Dictionary<Vector3Int, List<Vector3Int>>();

    public GameObject highlight;
    public GameObject selector;
    public Vector3Int playerPos;

    public GameObject player;
    //public List<HexTile> path;

    
    private void Start()
    {
        instance = this;
        tiles = new Dictionary<Vector3Int, CubeTile>();

        CubeTile[] cubeTiles = gameObject.GetComponentsInChildren<CubeTile>();
        // Register all the tiles
        foreach (CubeTile cubeTile in cubeTiles)
        {
            RegisterTile(cubeTile);
        }
        // Get each tiles set of neighbors
        foreach (CubeTile cubeTile in cubeTiles)
        {
            List<CubeTile> neighbors = GetNeighbours(cubeTile);
            cubeTile.neighbors = neighbors;
        }

        
        // Put the player somewhere
        int randomTile = Random.Range(0, cubeTiles.Length);
        CubeTile tile = cubeTiles[randomTile];
        /*
        while (tile.tileType != TileGenerationSettings.TileType.Standard)
        {
            tile = cubeTiles[randomTile];
        }
        /*
        playerPos = tile.gridCoordinates;
        */
        player.transform.position = tile.transform.position + new Vector3(0, 1f, 0);
        //player.currentTile = tile;
        
    }

    public void RegisterTile(CubeTile tile)
    {
        tiles.Add(tile.worldCoordinates, tile);
    }

    // Get the neighbors of each tile
    public List<CubeTile> GetNeighbours(CubeTile tile)
    {
        List<CubeTile> neighbors = new List<CubeTile>();

        Vector3Int[] neighborCoords = new Vector3Int[]
        {
            new Vector3Int(1, 0, 0),
            new Vector3Int(0, 0, 1),
            new Vector3Int(-1, 0, 0),
            new Vector3Int(0, 0, -1),
        };

        foreach (Vector3Int neighborCoord in neighborCoords)
        {
            Vector3Int tileCoord = tile.worldCoordinates;

            if (tiles.TryGetValue(tileCoord + neighborCoord, out CubeTile neighbor))
            {
                neighbors.Add(neighbor);
            }
        }
        return neighbors;
    }
    
    public List<Vector3Int> GetNeighboursFor(Vector3Int coordinates)
    {
        
        if(tiles.ContainsKey(coordinates) == false)
        {
            Debug.Log("No neighbors");
            return new List<Vector3Int>();
        }

        if (tiles.ContainsKey(coordinates))
        {
            Debug.Log("Yes neighbors");
            return tileNeighborDict[coordinates];
        }

        
        tileNeighborDict.Add(coordinates, new List<Vector3Int>());

        if (tiles.ContainsKey(coordinates))
        {
            tileNeighborDict[coordinates].Add(coordinates);
        }
        
        return tileNeighborDict[coordinates];
    }
    
    public void OnHighlightTile(Highlight tile)
    {
        highlight.transform.position = tile.transform.position;
    }

    public void OnSelectTile(Highlight tile)
    {
        selector.transform.position = tile.transform.position;
    }
    
}
