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

    GameObject TestFarmRegion;

    public void Start()
    {
        Debug.Log("Press 'C' to create a test farm region with 6 nonarable farm plots.");
        Debug.Log("(After pressing 'C') Press 'X' to convert a nonarable farm plot to an arable farm plot.");

        FarmPlots = new Dictionary<GameObject, List<FarmPlot>>();
    }

    public void Update()
    {
        // Test input for initializing farm functionality test
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Initialize farm test key pressed...");
            InitializeFarm_Test();
        }

        // Test input for land acquisition functionality
        if (Input.GetKeyDown(KeyCode.X) && TestFarmRegion != null)
        {
            Debug.Log("Acquire arable land test key pressed...");
            AcquireArableFarmPlot_Test(TestFarmRegion);
        }
    }

    // Convert a nonarable tile to an arable tile
    // Note: The method and process of farm expansion can be decided later.
    public void AcquireArableFarmPlot_Test(GameObject farmRegion)
    {
        List<FarmPlot> nonArableFarmPlots = new List<FarmPlot>();
        foreach (FarmPlot farmPlot in FarmPlots[farmRegion])
        {
            if (!farmPlot.IsArable)
                nonArableFarmPlots.Add(farmPlot);
        }

        if (nonArableFarmPlots.Count == 0)
        {
            Debug.Log("Cannot acquire more arable farm plots - no more nonarable farm plots exist.");
            return;
        }
            

        FarmPlot selectedFarmPlot = nonArableFarmPlots.ElementAt(
            Random.Range(0, nonArableFarmPlots.Count - 1));

        selectedFarmPlot.IsArable = true;

        Debug.Log("Farm plot at location (" + selectedFarmPlot.Location.x + ", " + selectedFarmPlot.Location.y + ") is now arable.");
    }

    void InitializeFarm_Test()
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
            testFarmPlot.Location = new Vector2 { x = coord.x, y = coord.y };

            // Set farm plot to be child of farm region object
            testFarmPlot.transform.parent = testFarmRegion.transform;

            testFarmPlots.Add(testFarmPlot);
        }

        FarmRegions = new List<GameObject> { testFarmRegion };
        FarmPlots[testFarmRegion] = testFarmPlots;
        TestFarmRegion = testFarmRegion;
        Debug.Log("FarmRegion and FarmPlots created.");
    }
}