using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    [SerializeField]
    Material material;
    [SerializeField]
    private Vector3[] Vertices;
    [SerializeField]
    private int[] Triangles;
    [SerializeField]
    private Vector3[] Normals;

    public void GenerateMesh()
    {
        GameObject gameObject = new GameObject("new mesh");
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = material;
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.sharedMesh = new Mesh();
        Mesh mesh = meshFilter.sharedMesh;
        mesh.vertices = Vertices;
        mesh.triangles = Triangles;
        mesh.normals = Normals;

        Terrain t;
    }    
}
