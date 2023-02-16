using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    [SerializeField] Terrain terrain;
    [SerializeField] Material material;
    [SerializeField] int blockSize;
    [SerializeField] int meshGridSize;

    List<Vector3> verts;
    List<int> tris;
    Vector2Int gridNumber;

    public void GenerateMesh()
    {
        gridNumber = new Vector2Int(0, 0);
        verts = new List<Vector3>();
        tris = new List<int>();
        Vector3 meshSize;
        float ratio = terrain.terrainData.detailResolution / terrain.terrainData.alphamapResolution;
        meshSize = terrain.terrainData.size;
        meshSize.x /= terrain.terrainData.detailResolution;
        meshSize.y /= terrain.terrainData.heightmapResolution;
        meshSize.z /= terrain.terrainData.detailResolution;

        for (int i = 0; i < terrain.terrainData.alphamapResolution; i += meshGridSize)
        {
            for(int j = 0; j < terrain.terrainData.alphamapResolution; j += meshGridSize)
            {
                verts.Add(new Vector3(i * meshSize.x * ratio, terrain.terrainData.GetHeight(i, j) * meshSize.y, j * meshSize.z * ratio));
                if (gridNumber.x == 0)
                    gridNumber.y++;
            }
            gridNumber.x++;
        }

        for(int i = 0; i < gridNumber.x - 1; i++)
        {
            for (int j = 0; j < gridNumber.y - 1; j++)
            {
                tris.Add(j + i * gridNumber.y);
                tris.Add(j + 1 + i * gridNumber.y);
                tris.Add(j + (i + 1) * gridNumber.y);

                tris.Add(j + (i + 1) * gridNumber.y);
                tris.Add(j + 1 + i * gridNumber.y);
                tris.Add((j + 1) + (i + 1) * gridNumber.y);
            }
        }

        GameObject gameObject = new GameObject("converted mesh");
        Mesh mesh = new Mesh();
        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = material;
        meshFilter.sharedMesh = mesh;
    }    
}
