using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class HexTile : MonoBehaviour
{
    public HexTileGenerationSettings settings;
    public HexTileGenerationSettings.TileType tileType;

    public GameObject tile;

    public Vector2Int offsetCoordinate;

    public Vector3Int cubeCoordinate;

    public List<HexTile> neighbors;

    private bool isDirty = false;

    private void OnValidate()
    {
        if (tile == null) { return; }

        isDirty = true;
    }

    private void Update()
    {
        if(isDirty)
        {
            if(Application.isPlaying)
            {
                GameObject.Destroy(tile);
            }
            else
            {
                GameObject.DestroyImmediate(tile);
            }

            AddTile();
            isDirty = false;
        }
    }

    public void RollTileType()
    {
        tileType = (HexTileGenerationSettings.TileType)Random.Range(0, 3);
    }

    public void AddTile()
    {
        tile = GameObject.Instantiate(settings.GetTile(tileType), transform);
        /*
        if (gameObject.GetComponent<MeshCollider>() == null)
        {
            MeshCollider collider = gameObject.AddComponent<MeshCollider>();
            collider.sharedMesh = GetComponentInChildren<MeshFilter>().mesh;
        }
        */
        transform.SetParent(tile.transform);
    }
    
    public void OnDrawGizmosSelected()
    {
        foreach (HexTile neighbor in neighbors)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, 0.1f);
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, neighbor.transform.position);
        }
    }
}
