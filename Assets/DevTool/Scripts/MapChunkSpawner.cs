using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChunkSpawner : MonoBehaviour
{
    public Vector2Int mapSize;
    public int chunkSize;

    public List<Chunk> chunks = new List<Chunk>();

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
    /*
    private void OnEnable()
    {
        LayoutGrid();
    }

    //[EditorButton("Roll")]
    public void LayoutGrid()
    {
        Clear();
        for (int y = 0; y < mapSize.y; y++)
        {
            for (int x = 0; x < mapSize.x; x++)
            {
                GameObject chunk = GameObject.Instantiate(chunks.GetRandom());
                chunk.name = $"Chunk (C{x},R{y})";
                chunk.transform.position = Utilities.GetPositionForHexFromCoordinate(chunkSize * x, chunkSize * y);
                //chunk.SetParent(transform, true);

                Vector2Int chunkCornerCoordinate = new Vector2Int(chunkSize * x, chunkSize * y);
                // Offset all the children by the cube coordinate
                foreach (Transform child in chunk.transform)
                {
                    HexTile tile = child.gameObject.GetComponent<HexTile>();
                    tile.offsetCoordinate += new Vector2Int(chunkCornerCoordinate.x, chunkCornerCoordinate.y);
                    tile.cubeCoordinate = Utilities.OffsetToCube(tile.offsetCoordinate);

                    tile.gameObject.name = $"Hex (C{tile.offsetCoordinate.x}, R{tile.offsetCoordinate.y})";
                }
            }
        }
    }
    */
}

[System.Serializable]
public class Chunk
{
    public GameObject chunkPrefab;

    public void GetRandom()
    {

    }
}