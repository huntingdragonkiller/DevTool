using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TileGeneration : MonoBehaviour
{
    [Header("Grid Settings")]
    public Vector2Int gridSize;
    public float radius;
    public bool isFlatTopped;

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
                Quaternion rotation = Quaternion.Euler(0f, 90f, 0f);
                GameObject tileGO = new GameObject($"Hex {x}, {y}");
                tileGO.transform.position = TileCoordinates.GetPositionForHexFromCoordinate(x, y, radius, isFlatTopped);

                TileGiver tile = tileGO.AddComponent<TileGiver>();
                tile.settings = settings;
                tile.RollTileType();
                tile.AddTile();

                if (isFlatTopped)
                {
                    tile.transform.rotation = rotation;
                }

                tileGO.transform.SetParent(transform);
            }
        }
    }

    public void Keep()
    {
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                // Get the parent and child GameObject
                GameObject parentTile = GameObject.Find($"Hex {x}, {y}");
                GameObject childTile = parentTile.transform.GetChild(0).gameObject;

                // Set the parent of the child GameObject to be the same as the parents
                childTile.transform.SetParent(parentTile.transform.parent, true);

                if(parentTile!=null)
                {
                    DestroyImmediate(parentTile);
                }
            }
        }
    }
}

[CustomEditor(typeof(TileGeneration))]
public class TileGenGUI : Editor
{
    public override void OnInspectorGUI()
    {
        TileGeneration tileGen = (TileGeneration)target;
        // add before
        base.OnInspectorGUI();
        // add after

        EditorGUILayout.LabelField("Layout", EditorStyles.boldLabel);
        if (GUILayout.Button("Clear"))
            tileGen.Clear();
        if (GUILayout.Button("Generate"))
            tileGen.LayoutGrid();
        if (GUILayout.Button("Keep"))
                tileGen.Keep();
    }
}
