using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    [Header("Grid Settings")]
    public Vector2Int gridSize;
    public float radius = 1f;
    public bool isFlatTopped;

    public HexTileGenerationSettings settings;

    // clears existing generation
    public void Clear()
    {
        List<GameObject> children = new List<GameObject>();

        for(int i=0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            children.Add(child);
        }

        foreach(GameObject child in children)
        {
            DestroyImmediate(child, true);
        }
    }
    
    // Clears pre existing grid generation and puts in a new one
    public void LayoutGrid()
    {
        Clear();
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                Quaternion rotation = Quaternion.Euler(0f, 90f, 0f);
                GameObject tile = new GameObject($"Hex {x}, {y}");
                tile.transform.position = HexUtilities.GetPositionForHexFromCoordinate(x, y, radius, isFlatTopped);
                
                HexTile hextile = tile.AddComponent<HexTile>();
                hextile.settings = settings;
                hextile.RollTileType();
                hextile.AddTile();

                // if flat top is selected rotate the hex to match new offset
                if (isFlatTopped)
                {
                    tile.transform.rotation = rotation;
                }

                // Assign its offset coordinates for human parsing ( Column, Row)
                hextile.offsetCoordinate = new Vector2Int(x, y);

                // Assign/convert these to cube coordinates for navigation
                hextile.cubeCoordinate = HexUtilities.OffsetToCube(hextile.offsetCoordinate);

                tile.transform.SetParent(transform);
            }
        }
    }
}
