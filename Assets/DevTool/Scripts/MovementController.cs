using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public List<HexTile> currentPath;
    public HexTile nextTile;
    public HexTile currentTile;
    public Vector3 targetPosition;
    public bool gotPath;

    public LineRenderer _renderer;
    
    protected void UpdateLineRenderer(List<HexTile> tiles)
    {
        if (_renderer == null) { return; }

        List<Vector3> points = new List<Vector3>();
        foreach (HexTile tile in tiles)
        {
            points.Add(tile.transform.position + new Vector3(0, 0.5f, 0));
        }
        _renderer.positionCount = points.Count;
        _renderer.SetPositions(points.ToArray());
    }
    
    public void HandleMovement()
    {
        if(currentPath == null || currentPath.Count <= 1)
        {
            nextTile = null;

            if(currentPath!= null && currentPath.Count > 0)
            {
                currentTile = currentPath[0];
                nextTile = currentTile;
            }

            gotPath = false;
            UpdateLineRenderer(new List<HexTile>());
        }
        else
        {
            currentTile = currentPath[0];

            nextTile = currentPath[1];
            // If the next tile is non traversable, stop moving;
            if(nextTile.tileType!= HexTileGenerationSettings.TileType.Standard)
            {
                currentPath.Clear();
                HandleMovement();
                return;
            }

            targetPosition = nextTile.transform.position + new Vector3(0, 1f, 0);
            gotPath = true;
            currentPath.RemoveAt(0);
            TileManager.instance.playerPos = nextTile.cubeCoordinate;
            UpdateLineRenderer(currentPath);
        }
    }
}
