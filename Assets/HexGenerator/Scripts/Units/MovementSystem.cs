using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    private BFSResult movementRange = new BFSResult();
    private List<Vector3Int> currentPath = new List<Vector3Int>();

    public void HideRange(TileGrid tileGrid)
    {
        foreach (Vector3Int tilePosition in movementRange.GetRangePositions())
        {
            tileGrid.GetTileAt(tilePosition).DisableHighlight();
        }
        movementRange = new BFSResult();
    }

    public void ShowRange(Unit selectedUnit, TileGrid tileGrid)
    {
        CalculateRange(selectedUnit, tileGrid);

        foreach (Vector3Int tilePosition in movementRange.GetRangePositions() )
        {
            tileGrid.GetTileAt(tilePosition).EnableHighlight();
        }
    }

    private void CalculateRange(Unit selectedUnit, TileGrid tileGrid)
    {
        movementRange = GraphSearch.BFSGetRange(tileGrid, tileGrid.GetClosestHex(selectedUnit.transform.position), selectedUnit.MovementPoints);
    }

    public void ShowPath(Vector3Int selectedTilePosition, TileGrid tileGrid)
    {
        if(movementRange.GetRangePositions().Contains(selectedTilePosition))
        {
            foreach(Vector3Int tilePosition in currentPath)
            {
                tileGrid.GetTileAt(tilePosition).ResetHighlight();
            }
            currentPath = movementRange.GetPathTo(selectedTilePosition);
            foreach (Vector3Int tilePosition in currentPath)
            {
                tileGrid.GetTileAt(tilePosition).HighlightPath();
            }
        }
    }

    public void MoveUnit(Unit selectedUnit, TileGrid tileGrid)
    {
        Debug.Log("Moving unit " + selectedUnit.name);
        selectedUnit.MoveThroughPath(currentPath.Select(pos => tileGrid.GetTileAt(pos).transform.position).ToList());
    }

    public bool IsTileInRange(Vector3Int tilePosition)
    {
        return movementRange.IsTilePositionInRange(tilePosition);
    }
}
