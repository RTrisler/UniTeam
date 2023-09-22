using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FarmPlotStateController : MonoBehaviour
{
    // Public fields
    public List<GameObject> FarmRegions;
    //  Note: Child objects of FarmRegion GameObjects are GameObjects with FarmPlot components

    // Inspector fields
    [SerializeField]
    MapTileManager MapTileManager;
    [SerializeField]
    GameObject _prefab_FarmRegion;
    [SerializeField]
    GameObject _prefab_FarmPlot;

    // Private fields
    Dictionary<GameObject, List<FarmPlot>> FarmPlots;
    //  Note: Dictionary maps FarmRegion GameObject to its children object's FarmPlot components
    //          => FarmPlots[FarmRegions.First()] = list of FarmPlot components from a farm region's child objects
    bool IsTestFarmCreated;

    public void Start()
    {
        Debug.Log("TEST FARM SETUP:\n\n\n1. Press 'C' to create a test farm region with 6 nonarable farm plots.\n2. (After pressing 'C') Press 'X' to convert a nonarable farm plot to an arable farm plot.\n\n\nPLAYER-FARMPLOT INTERACTIONS:\n\n\n1. When standing on an arable farm plot, press 'T' to till the land.\n2. When standing on a tilled farm plot, press 'P' to plant a seed on the farm plot.\n3. To progress plant growth on all sown farm plots, press 'Z'.\n4. When standing on a farm plot with a fully grown plant, press 'H' to harvest and clear the farm plot.");

        FarmPlots = new Dictionary<GameObject, List<FarmPlot>>();

        IsTestFarmCreated = false;
    }

    public void Update()
    {
        /* TEST FARM SETUP */
        // Test input for initializing farm functionality test
        if (Input.GetKeyDown(KeyCode.C) && !IsTestFarmCreated)
        {
            Debug.Log("DEBUG COMMAND: Initialize farm test key pressed...");
            IsTestFarmCreated = true;
            DEBUG_InitializeFarmTest();
        }
        else if (Input.GetKeyDown(KeyCode.C) && IsTestFarmCreated)
        {
            Debug.Log("DEBUG COMMAND: Initialize farm test key pressed, but the test farm has already been created. Doing nothing.");
        }

        // Test input for land acquisition functionality
        if (Input.GetKeyDown(KeyCode.X) && FarmRegions.Count > 0)
        {
            Debug.Log("DEBUG COMMAND: Acquire arable land test key pressed...");
            DEBUG_AcquireArableFarmPlot(FarmRegions.First());
        }

        // Test input for progressing plant growth
        if (Input.GetKeyDown(KeyCode.Z))
        {
            DEBUG_ProgressCropGrowth();
        }


        /* PLAYER FARM INTERACTIONS */
        if (Input.GetKeyDown(KeyCode.T))
        {
            TillFarmPlot();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            PlantSeed();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            HarvestPlant();
        }
    }

    // Plant a seed on a tilled farm plot. Changes plot state from Grown > Arable.
    // Note: Harvest plant restarts the state cycle of the farm plot.
    public void HarvestPlant()
    {
        FarmPlot targetFarmPlot = GetTargetFarmPlot(FarmPlotState.Grown);

        if (targetFarmPlot != null)
        {
            // Include logic to interact with player inventory here
            // if (plant gives weapon) { give player weapon } ... etc.
            targetFarmPlot.State = FarmPlotState.Arable;
            Debug.Log("Plant harvested from farm plot at location (" + targetFarmPlot.Location.x + ", " + targetFarmPlot.Location.y + ").");
        }
        else
            Debug.Log("Player is not standing on a farm plot with a fully grown plant. There is no plant to harvest here. I am deeply upset.");
    }

    // Plant a seed on a tilled farm plot. Changes plot state from Tilled > Sown
    // Note: The transition from Sown > Grown is undecided. We are considering hinging
    //       plant growth on the amount of dungeon runs there are.
    public void PlantSeed()
    {
        FarmPlot targetFarmPlot = GetTargetFarmPlot(FarmPlotState.Tilled);

        if (targetFarmPlot != null)
        {
            // Include logic to interact with player inventory here
            // if (player has seeds) { ... } ... etc.
            targetFarmPlot.State = FarmPlotState.Sown;
            Debug.Log("Seed planted on farm plot at location (" + targetFarmPlot.Location.x + ", " + targetFarmPlot.Location.y + ").");
        }

        else
            Debug.Log("Player is not standing on a tilled farm plot. Nowhere to plant my awesome seeds. This is terrible.");
    }

    // Till a farm plot. Changes plot state from Arable > Tilled
    public void TillFarmPlot()
    {
        FarmPlot targetFarmPlot = GetTargetFarmPlot(FarmPlotState.Arable);

        if (targetFarmPlot != null)
        {
            targetFarmPlot.State = FarmPlotState.Tilled;
            Debug.Log("Tile at location (" + targetFarmPlot.Location.x + ", " + targetFarmPlot.Location.y + ") tilled.");
        }

        else
            Debug.Log("Player is not standing on an arable farm plot. Nothing to till here!");
    }

    FarmPlot GetTargetFarmPlot(FarmPlotState searchState)
    {
        // If player is standing on farm tile
        List<FarmPlot> farmPlots = FarmPlots[FarmRegions.First()];

        FarmPlot targetFarmPlot = null;
        foreach (FarmPlot farmPlot in farmPlots)
        {
            if (MapTileManager.PlayerCurrentTile == farmPlot.Location && farmPlot.State == searchState)
            {
                // Player is standing on the target farm plot, and the farm plot state is appropriate
                targetFarmPlot = farmPlot;
                break;   
            }
        }

        return targetFarmPlot;
    }

    // Convert a farm plot. Changes plot state from Nonarable > Arable
    // Note: The method and process of farm expansion can be decided later.
    //       Currently, tile states from the test hardcoded farm region are randomly swapped
    public void DEBUG_AcquireArableFarmPlot(GameObject farmRegion)
    {
        List<FarmPlot> nonArableFarmPlots = new List<FarmPlot>();
        foreach (FarmPlot farmPlot in FarmPlots[farmRegion])
        {
            if (farmPlot.State == FarmPlotState.Nonarable)
                nonArableFarmPlots.Add(farmPlot);
        }

        if (nonArableFarmPlots.Count == 0)
        {
            Debug.Log("DEBUG COMMAND: No more nonarable farm plots exist, cannot acquire any more farm plots.");
            return;
        }

        FarmPlot selectedFarmPlot = nonArableFarmPlots.ElementAt(
            Random.Range(0, nonArableFarmPlots.Count - 1));

        selectedFarmPlot.State = FarmPlotState.Arable;

        Debug.Log("DEBUG COMMAND: Farm plot at location (" + selectedFarmPlot.Location.x + ", " + selectedFarmPlot.Location.y + ") is now arable.");
    }

    // Initialize game objects to track farm regions + farm tiles and state
    void DEBUG_InitializeFarmTest()
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

        // Create FarmRegion GameObject - children of FarmRegion are GameObjects with FarmPlot component
        GameObject testFarmRegion = Instantiate(_prefab_FarmRegion, new Vector3(0,0,0), Quaternion.identity);

        List<FarmPlot> testFarmPlots = new List<FarmPlot>();
        foreach (Vector3Int coord in testFarmPlotCoordinates)
        {
            // Instantiate FarmPlot GameObjects, assign location acoordingly
            FarmPlot testFarmPlot = Instantiate(_prefab_FarmPlot, new Vector3(0,0,0), Quaternion.identity).GetComponent<FarmPlot>();
            testFarmPlot.Location = coord;

            // Set farm plot to be child of farm region object
            testFarmPlot.transform.parent = testFarmRegion.transform;

            testFarmPlots.Add(testFarmPlot);
        }

        FarmRegions = new List<GameObject> { testFarmRegion };
        FarmPlots[testFarmRegion] = testFarmPlots;
        Debug.Log("FarmRegion and FarmPlots created.");
    }

    public void DEBUG_ProgressCropGrowth()
    {
        List<FarmPlot> farmPlots = FarmPlots[FarmRegions.First()];

        foreach (FarmPlot farmPlot in farmPlots)
        {
            if (farmPlot.State == FarmPlotState.Sown)
            {
                farmPlot.State = FarmPlotState.Grown;
            }
        }

        Debug.Log("DEBUG COMMAND: Progressed plant growth. Sown plots are now ready for harvest.");
    }
}