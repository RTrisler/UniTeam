using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class FarmInteractions : MonoBehaviour
{
    [SerializeField]
    MapTileManager MapTileManager;
    public List<Vector3Int> TestFarmCoordinates;

    // Start is called before the first frame update
    void Start()
    {
        InputController.Instance.playerActionMap.FarmInteract.performed += OnFarmInteract;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnFarmInteract(InputAction.CallbackContext context)
    {
        foreach (Vector3Int farmPlotCoordinate in MapTileManager.TileGridCoordinates)
        {
            if (MapTileManager.PlayerCurrentTile == farmPlotCoordinate)
            {
                Debug.Log("Pressed Q, standing on: " + farmPlotCoordinate);

                FarmPlot targetFarmPlot = MapTileManager.FarmPlots.First(plot => plot.Location == farmPlotCoordinate);

                switch (targetFarmPlot.State)
                {
                    case FarmPlotState.Arid:
                        Debug.Log("This plot is arid, nothing can be planted here!");
                        break;
                    case FarmPlotState.Arable:
                        Debug.Log("This plot is arable, anything can be planted here!");
                        // Remove 1 seed from inventory
                        Debug.Log("Planting seed...");
                        // Place seed graphic on farm tile
                        MapTileManager.PlantSeed(farmPlotCoordinate);
                        break;
                    case FarmPlotState.Growing:
                        Debug.Log("This plot is growing...");
                        MapTileManager.GrowSeed(farmPlotCoordinate);
                        Debug.Log("The plant is done growing!");
                        break;
                    case FarmPlotState.Grown:
                        Debug.Log("Harvesting plant!");
                        MapTileManager.HarvestPlant(farmPlotCoordinate);
                        Debug.Log("Plant added to inventory!");
                        break;
                }
                
                break;
            }
        }
        
        Debug.Log("I'm doing it! I'm really doing it!!");
    }
}