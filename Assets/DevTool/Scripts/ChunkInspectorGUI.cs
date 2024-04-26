using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HexGrid))]
public class ChunkInspectorGUI : Editor
{
    public override void OnInspectorGUI()
    {
        HexGrid hgrid = (HexGrid)target;
        // add before
        base.OnInspectorGUI();
        // add after

        EditorGUILayout.LabelField("Layout", EditorStyles.boldLabel);
        if (GUILayout.Button("Clear"))
            hgrid.Clear();
        if (GUILayout.Button("Generate"))
        {
            hgrid.LayoutGrid();
        }
    }
}
