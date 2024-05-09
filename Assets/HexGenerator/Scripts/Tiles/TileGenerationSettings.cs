using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TileGen/TileGenerationSettings")]
public class TileGenerationSettings : ScriptableObject
{
    public enum TileType
    {
        Standard,
        Water,
        Cliff
    }

    public GameObject Standard;
    public GameObject Water;
    public GameObject Cliff;

    public GameObject GetTile(TileType tileType)
    {
        switch (tileType)
        {
            case TileType.Standard:
                return Standard;
            case TileType.Water:
                return Water;
            case TileType.Cliff:
                return Cliff;
        }
        return null;
    }
}
