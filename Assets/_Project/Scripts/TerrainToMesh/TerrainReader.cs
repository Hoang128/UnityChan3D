using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainReader : MonoBehaviour
{
    [SerializeField]
    Terrain terrain;

    public void GetTerrainInfo()
    {
        Debug.Log("terrain data info:");
        Debug.Log("size = " + terrain.terrainData.size);
        Debug.Log("detail res = " + terrain.terrainData.detailResolution);
        Debug.Log("detail res per patch = " + terrain.terrainData.detailResolutionPerPatch);
        Debug.Log("alpha map res = " + terrain.terrainData.alphamapResolution);

        Debug.Log("height map res = " + terrain.terrainData.heightmapResolution);
        Debug.Log("height map texture = " + terrain.terrainData.heightmapTexture);
    }
}
