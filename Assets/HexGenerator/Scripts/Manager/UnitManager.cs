using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [SerializeField]
    private TileGrid tileGrid;

    [SerializeField]
    private MovementSystem movementSystem;

    public bool PlayersTurn { get; private set; } = true;

    [SerializeField]
    private Unit selectedUnit;
    private Tile previouslySelectedTile;

    // invoke broke
    public void HandleUnitSelected(GameObject unit)
    {
        if (PlayersTurn == false)
            return;

        Unit unitReference = unit.GetComponent<Unit>();

        if (CheckIfTheSameUnitSelected(unitReference))
            return;

        PrepareUnitForMovement(unitReference);
    }

    private bool CheckIfTheSameUnitSelected(Unit unitReference)
    {
        if (this.selectedUnit == unitReference)
        {
            ClearOldSelection();
            return true;
        }
        return false;
    }

    // invoke broke
    public void HandleTerrainSelected(GameObject tileGO)
    {
        if (selectedUnit == null || PlayersTurn == false)
        {
            return;
        }

        Tile selectedTile = tileGO.GetComponent<Tile>();

        if (HandleTileOutOfRange(selectedTile.tileCoords) || HandleSelectedTileIsUnitTile(selectedTile.tileCoords))
            return;

        HandleTargetTileSelected(selectedTile);
    }

    private void PrepareUnitForMovement(Unit unitReference)
    {
        if (this.selectedUnit != null)
        {
            ClearOldSelection();
        }
        this.selectedUnit = unitReference;
        
        this.selectedUnit.Select();
        movementSystem.ShowRange(this.selectedUnit, this.tileGrid);
    }

    private void ClearOldSelection()
    {
        previouslySelectedTile = null;
        this.selectedUnit.Deselect();
        movementSystem.HideRange(this.tileGrid);
        this.selectedUnit = null;
    }

    private void HandleTargetTileSelected(Tile selectedTile)
    {
        if (previouslySelectedTile == null || previouslySelectedTile != selectedTile)
        {
            previouslySelectedTile = selectedTile;
            movementSystem.ShowPath(selectedTile.tileCoords, this.tileGrid);
        }
        else
        {
            movementSystem.MoveUnit(selectedUnit, this.tileGrid);
            PlayersTurn = false;
            selectedUnit.MovementFinished += ResetTurn;
            ClearOldSelection();
        }
    }

    private bool HandleSelectedTileIsUnitTile(Vector3Int tilePosition)
    {
        if (tilePosition == tileGrid.GetClosestHex(selectedUnit.transform.position))
        {
            selectedUnit.Deselect();
            ClearOldSelection();
            return true;
        }
        return false;
    }

    private bool HandleTileOutOfRange(Vector3Int tilePosition)
    {
        if (movementSystem.IsTileInRange(tilePosition) == false)
        {
            Debug.Log("Tile out of range!");
            return true;
        }
        return false;
    }

    private void ResetTurn(Unit selectedUnit)
    {
        selectedUnit.MovementFinished -= ResetTurn;
        PlayersTurn = true;
    }
}
