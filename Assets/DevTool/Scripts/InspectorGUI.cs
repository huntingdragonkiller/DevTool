using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HexGrid))]
public class InspectorGUI : Editor
{
    public override void OnInspectorGUI()
    {
        HexGrid hgrid = (HexGrid)target;
        // add before
        base.OnInspectorGUI();
        // add after

        if (GUILayout.Button("Clear"))
            hgrid.Clear();
        if (GUILayout.Button("Layout"))
        {
            hgrid.LayoutGrid();
        }
    }
}
