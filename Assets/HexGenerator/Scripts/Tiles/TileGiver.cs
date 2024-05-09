using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TileGiver : MonoBehaviour
{
    public TileGenerationSettings.TileType tileType;
    public TileGenerationSettings settings;

    public GameObject tile;

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
        tile = GameObject.Instantiate(settings.GetTile(tileType), transform);
        transform.SetParent(tile.transform);
    }
}
