using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Tile : MonoBehaviour
{
    [SerializeField]
    private GlowHighlight highlight;
    private TileCoordinates tileCoordinates;

    [SerializeField]
    private TileType tileTravelType;

    public Vector3Int tileCoords => tileCoordinates.GetTileCoords();

    public int GetCost()
        => tileTravelType switch
        {
            TileType.Difficult => 20,
            TileType.Default => 10,
            TileType.Road => 5,
            TileType.Water => 9999,
            _ => throw new Exception($"Tile of type {tileTravelType} not supported")
        };

    public bool IsObstacle()
    {
        return this.tileTravelType == TileType.Obstacle;
    }

    private void Awake()
    {
        tileCoordinates = GetComponent<TileCoordinates>();
        highlight = GetComponent<GlowHighlight>();
    }

    public void EnableHighlight()
    {
        highlight.ToggleGlow(true);
    }
    public void DisableHighlight()
    {
        highlight.ToggleGlow(false);
    }

    internal void ResetHighlight()
    {
        highlight.ResetGlowHighlight();
    }

    internal void HighlightPath()
    {
        highlight.HighlightValidPath();
    }
    
}

public enum TileType
{
    None,
    Default,
    Difficult,
    Road,
    Water,
    Obstacle
}
