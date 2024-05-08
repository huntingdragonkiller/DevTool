using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTileManager : MonoBehaviour
{
    public static CubeTileManager instance;
    public Dictionary<Vector2, CubeTile> tiles;

    public GameObject highlight;
    public GameObject selector;
    public Vector2 playerPos;

    public GameObject player;
    //public List<HexTile> path;

    
    private void Awake()
    {
        instance = this;
        tiles = new Dictionary<Vector2, CubeTile>();

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
        tiles.Add(tile.gridCoordinates, tile);
    }

    // Get the neighbors of each tile
    private List<CubeTile> GetNeighbours(CubeTile tile)
    {
        List<CubeTile> neighbors = new List<CubeTile>();

        Vector2[] neighborCoords = new Vector2[]
        {
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(-1, 0),
            new Vector2(0, -1),
        };

        foreach (Vector2 neighborCoord in neighborCoords)
        {
            Vector2 tileCoord = tile.gridCoordinates;

            if (tiles.TryGetValue(tileCoord + neighborCoord, out CubeTile neighbor))
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
    
}
