using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexHighlight : MonoBehaviour
{
    public void OnHighlightTile()
    {
        TileManager.instance.OnHighlightTile(this);
    }

    public void OnSelectTile()
    {
        TileManager.instance.OnSelectTile(this);
    }
}
