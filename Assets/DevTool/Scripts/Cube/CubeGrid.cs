using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CubeGrid : MonoBehaviour
{
    [Header("Grid Settings")]
    public Vector2Int gridSize;
    public int radius;

    public TileGenerationSettings settings;

    // clears existing generation
    public void Clear()
    {
        List<GameObject> children = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            children.Add(child);
        }

        foreach (GameObject child in children)
        {
            DestroyImmediate(child, true);
        }
    }


    // Clears pre existing grid generation and puts in a new one
    public void LayoutGrid()
    {
        Vector3 position = Vector3.zero;
        //Vector2 tileSize = TileSize();
        Clear();
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                GameObject tile = new GameObject($"Tile {x}, {y}");
                Vector3Int tilePosition = new Vector3Int(x * radius, 0, y * radius);
                tile.transform.position = tilePosition;

                CubeTile cubetile = tile.AddComponent<CubeTile>();
                cubetile.settings = settings;
                cubetile.RollTileType();
                cubetile.AddTile();

                if (cubetile.tileType == TileGenerationSettings.TileType.Standard)
                    tile.layer = 7;
                tile.GetComponent<CubeTile>().gridCoordinates = new Vector2Int(x, y);
                tile.GetComponent<CubeTile>().worldCoordinates = new Vector3Int(x, 0, y);
                tile.transform.SetParent(transform);
            }
        }
    }
}

[CustomEditor(typeof(CubeGrid))]
public class CubeChunkGenGUI : Editor
{
    public override void OnInspectorGUI()
    {
        CubeGrid cgrid = (CubeGrid)target;
        // add before
        base.OnInspectorGUI();
        // add after

        EditorGUILayout.LabelField("Layout", EditorStyles.boldLabel);
        if (GUILayout.Button("Clear"))
            cgrid.Clear();
        if (GUILayout.Button("Generate"))
        {
            cgrid.LayoutGrid();
        }
    }
}
