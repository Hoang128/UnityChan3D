using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCtrl : MonoBehaviour
{
    Terrain terrain;

    // Start is called before the first frame update
    void Start()
    {
        terrain = GetComponent<Terrain>();
        PrintTerrainPros();
    }

    void PrintTerrainPros()
    {
        Debug.Log("bake light probes for trees = " + terrain.bakeLightProbesForTrees);
        Debug.Log("base map distance = " + terrain.basemapDistance);
        Debug.Log("bottom neighbor = " + terrain.bottomNeighbor);
    }
}
