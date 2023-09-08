using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FarmPlotStateController : MonoBehaviour
{
    [SerializeField]
    List<GameObject> FarmRegions;

    Dictionary<GameObject, List<GameObject>> Farms;

    GameObject TestRegion;

    public void Start()
    {
        // Create and populate a dictionary mapping farm regions to their set of farm plots
        Farms = new Dictionary<GameObject, List<GameObject>>();

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
    }

    public void Update()
    {
        // Test input for land acquisition functionality
        if (Input.GetKeyDown(KeyCode.C))
            AcquireArableFarmland(TestRegion);
    }

    // Convert a nonarable tile to an arable tile
    // Note: The method and process of farm expansion can be decided later.
    public void AcquireArableFarmland(GameObject farmRegion)
    {
        List<GameObject> farmPlots = Farms[farmRegion];

        List<FarmPlot> nonArableFarmPlots = new List<FarmPlot>();
        foreach (GameObject farmPlot in farmPlots)
        {
            FarmPlot farmPlot = farmPlot.GetComponent<FarmPlot>();
            if (!farmPlot.IsArable)
                nonArableFarmPlots.Add(farmPlot);
        }

        if (nonArableFarmPlots.Count == 0)
            return;
        
        // Select random nonarable plot of farmland from list of nonarable plots of farmland
        FarmPlot selectedFarmPlot = nonArablePlots.ElementAt(
            Random.Range(0, nonArableFarmPlots.Count - 1));

        // Update the farmland to be arable, and update the tile sprite to represent arable land
        selectedFarmRegionPlot.IsArable = true;
        selectedFarmRegionPlot.gameObject.GetComponent<SpriteRenderer>()
            .color = new Color(0.1152545f, 0.6603774f, 0.2449462f, 1.0f);
    }
}
