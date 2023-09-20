using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FarmPlotStateController : MonoBehaviour
{
    //[SerializeField]
    //List<GameObject> FarmRegions;
    [SerializeField]
    MapTileManager MapTileManager;

    [SerializeField]
    GameObject _prefab_FarmRegion;

    Dictionary<List<Vector3Int>, List<GameObject>> Farms;

    GameObject TestRegion;

    public void Start()
    {
        Debug.Log("Press \"C\" to make a non-arable plot of farm land arable.");
        // Create and populate a dictionary mapping farm regions to their set of farm plots
        
        Farms = new Dictionary<List<Vector3Int>, List<GameObject>>();
        /*
        foreach (GameObject farmRegion in FarmRegions)
        {
            List<GameObject> farmRegionPlots = new List<GameObject>();
            for (int i = 0; i < farmRegion.transform.childCount; i++)
            {
                farmRegionPlots.Add(farmRegion.transform.GetChild(i).gameObject);
            }
            Farms[farmRegion] = farmRegionPlots;
            TestRegion = farmRegion;
        }
        */
    }

    public void Update()
    {
        // Test input for initializing farm functionality test
        if (Input.GetKeyDown(KeyCode.X))
            InitializeFarmTest();

        // Test input for land acquisition functionality
        if (Input.GetKeyDown(KeyCode.C))
            AcquireArableFarmland(TestRegion);
    }

    // Convert a nonarable tile to an arable tile
    // Note: The method and process of farm expansion can be decided later.
    public void AcquireArableFarmland(GameObject farmRegion)
    {
        /*
        List<GameObject> farmRegionPlots = Farms[farmRegion];

        List<FarmPlot> nonArableFarmPlots = new List<FarmPlot>();
        foreach (GameObject farmRegionPlot in farmRegionPlots)
        {
            FarmPlot farmPlot = farmRegionPlot.GetComponent<FarmPlot>();
            if (!farmPlot.IsArable)
                nonArableFarmPlots.Add(farmPlot);
        }

        if (nonArableFarmPlots.Count == 0)
            return;
        
        // Select random nonarable plot of farmland from list of nonarable plots of farmland
        FarmPlot selectedFarmPlot = nonArableFarmPlots.ElementAt(
            Random.Range(0, nonArableFarmPlots.Count - 1));

        // Update the farmland to be arable, and update the tile sprite to represent arable land
        selectedFarmPlot.IsArable = true;
        selectedFarmPlot.gameObject.GetComponent<SpriteRenderer>()
            .color = new Color(0.1152545f, 0.6603774f, 0.2449462f, 1.0f);
        */
    }

    void InitializeFarmTest()
    {
        List<Vector3Int> testFarmPlotCoordinates = new List<Vector3Int>();
        for (int x = -7; x <= -5; x++)
        {
            for (int y = -3; y <= -2; y++)
            {
                Vector3Int localPlace = (new Vector3Int(x, y, (int)MapTileManager.Tilemap.transform.position.y));
                if (MapTileManager.Tilemap.HasTile(localPlace))
                {
                    testFarmPlotCoordinates.Add(localPlace);
                }
            }
        }

        GameObject testFarmRegion = Instantiate(_prefab_FarmRegion, new Vector3(0,0,0), Quaternion.identity);
        foreach (Vector3Int coord in testFarmPlotCoordinates)
        {
            Instantiate(_prefab_FarmPlot), new Vector3(0,0,0), Quaternion.identity);
        }

        Farms[testFarmPlotCoordinates] = GetTestFarmGameObjects();
    }

    List<GameObject> GetTestFarmGameObjects()
    {
        return new List<GameObject>();
    }
}
