using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TerrainReader))]
public class TerrainReaderEditor : Editor
{
    private TerrainReader terrainReader;

    public void OnEnable()
    {
        terrainReader = (TerrainReader)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Read terrain data"))
        {
            terrainReader.GetTerrainInfo();
        }
    }
}
