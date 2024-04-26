using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Playables;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HexGrid : MonoBehaviour
{
    [Header("Grid Settings")]
    public Vector2Int gridSize;
    public float radius = 1f;
    public bool isFlatTopped;

    public HexTileGenerationSettings settings;


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

    
    public void LayoutGrid()
    {
        Clear();
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                GameObject tile = new GameObject($"Hex {x}, {y}");
                tile.transform.position = Utilities.GetPositionForHexFromCoordinate(x, y, radius, isFlatTopped);
                
                HexTile hextile = tile.AddComponent<HexTile>();
                hextile.settings = settings;
                hextile.RollTileType();
                hextile.AddTile();
                

                // Assign its offset coordinates for human parsing ( Column, Row)
                hextile.offsetCoordinate = new Vector2Int(x, y);

                // Assign/convert these to cube coordinates for navigation
                hextile.cubeCoordinate = Utilities.OffsetToCube(hextile.offsetCoordinate);

                tile.transform.SetParent(transform);
            }
        }
    }
}
