using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
public class CubeTile : MonoBehaviour
{
    public TileGenerationSettings settings;
    public TileGenerationSettings.TileType tileType;

    public GameObject tile;

    public Vector2Int gridCoordinates;

    public Vector3Int worldCoordinates;

    public List<CubeTile> neighbors;

    private bool isDirty = false;

    private void OnValidate()
    {
        if (tile == null) { return; }

        isDirty = true;
    }

    private void Update()
    {
        if (isDirty)
        {
            if (Application.isPlaying)
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
        tileType = (TileGenerationSettings.TileType)Random.Range(0, 3);
    }

    public void AddTile()
    {
        tile = Instantiate(settings.GetTile(tileType), transform);
        transform.SetParent(tile.transform);
    }

    
    // Draw lines to each tile neighbors
    public void OnDrawGizmosSelected()
    {
        foreach (CubeTile neighbor in neighbors)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, 0.1f);
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, neighbor.transform.position);
        }
    }
    
}
