using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    public void OnHighlightTile()
    {
        HexTileManager.instance.OnHighlightTile(this);
    }

    public void OnSelectTile()
    {
        HexTileManager.instance.OnSelectTile(this);
    }
}
