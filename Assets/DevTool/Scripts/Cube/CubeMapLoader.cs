using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CubeMapLoader : MonoBehaviour
{
    public Vector2Int mapSize;
    private int chunkSize;

    public List<GameObject> chunks = new List<GameObject>();

    // deletes existing generation
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

    // functions the same as HexGrid script, except uses prefabs of pre existing chunks
    public void LayoutGrid()
    {
        Clear();
        for (int y = 0; y < mapSize.y; y++)
        {
            for (int x = 0; x < mapSize.x; x++)
            {
                int randomChunk = Random.Range(0, chunks.Count);
                GameObject chunk = Instantiate(chunks[randomChunk]);
                chunkSize = chunk.GetComponent<CubeGrid>().radius * chunk.GetComponent<CubeGrid>().gridSize.x;
                chunk.name = $"Chunk (C{x},R{y})";
                Vector3Int chunkPosition = new Vector3Int(x * chunkSize, 0, y * chunkSize);
                chunk.transform.position = chunkPosition;
                chunk.transform.SetParent(transform, true);

                Vector2Int chunkCornerCoordinate = new Vector2Int(chunkSize/chunk.GetComponent<CubeGrid>().radius * x, chunkSize * y / chunk.GetComponent<CubeGrid>().radius * y);
                // Offset all the children by the cube coordinate
                foreach (Transform child in chunk.transform)
                {
                    CubeTile tile = child.gameObject.GetComponent<CubeTile>();
                    tile.gridCoordinates += new Vector2Int(chunkCornerCoordinate.x, chunkCornerCoordinate.y);
                    tile.worldCoordinates += new Vector3Int(chunkCornerCoordinate.x, 0, chunkCornerCoordinate.y);

                    tile.gameObject.name = $"Cube (C{tile.gridCoordinates.x}, R{tile.gridCoordinates.y})";
                }
            }
        }
    }
}

[CustomEditor(typeof(CubeMapLoader))]
class CubeMapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CubeMapLoader mapChunkSpawner = (CubeMapLoader)target;
        base.OnInspectorGUI();
        EditorGUILayout.LabelField("Layout", EditorStyles.boldLabel);
        if (GUILayout.Button("Clear"))
        {
            mapChunkSpawner.Clear();
        }
        if (GUILayout.Button("Generate"))
        {
            mapChunkSpawner.LayoutGrid();
        }
    }
}
