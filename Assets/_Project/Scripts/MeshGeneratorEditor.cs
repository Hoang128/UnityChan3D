using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MeshGenerator))]
//[ExecuteInEditMode]
public class MeshGeneratorEditor : Editor
{
    private MeshGenerator meshGenerator;

    private void OnEnable()
    {
        meshGenerator = (MeshGenerator)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        if (GUILayout.Button("Generate Mesh Object"))
        {
            meshGenerator.GenerateMesh();
        }
    }
}
