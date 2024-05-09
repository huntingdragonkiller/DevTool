using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class TileMapGen : MonoBehaviour
{
    public Vector2Int mapSize;
    public int chunkSize;

    public List<GameObject> chunks = new List<GameObject>();

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

    public void LayoutGrid()
    {
        Clear();
        for(int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                int randomChunk = Random.Range(0, chunks.Count);
                GameObject chunk = Instantiate(chunks[randomChunk]);
                chunk.name = $"Chunk (C{x}, R{y})";
                chunk.transform.position = TileCoordinates.GetPositionForHexFromCoordinate(x, y, chunkSize);
                chunk.transform.SetParent(transform, true);
            }
        }
            
    }
}

[CustomEditor(typeof(TileMapGen))]
public class TileMapGenGUI : Editor
{
    public override void OnInspectorGUI()
    {
        TileMapGen tileGen = (TileMapGen)target;
        // add before
        base.OnInspectorGUI();
        // add after

        EditorGUILayout.LabelField("Layout", EditorStyles.boldLabel);
        if (GUILayout.Button("Clear"))
            tileGen.Clear();
        if (GUILayout.Button("Generate"))
            tileGen.LayoutGrid();
    }
}
