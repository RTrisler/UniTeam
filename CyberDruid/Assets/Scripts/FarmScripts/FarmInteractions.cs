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
        foreach (Vector3Int coordinate in MapTileManager.TileGridCoordinates)
        {
            if (MapTileManager.PlayerCurrentTile == coordinate)
            {
                Debug.Log("Pressed Q, standing on: " + coordinate);

                FarmPlot targetFarmPlot = MapTileManager.FarmPlots.First(plot => plot.Location == coordinate);

                switch (targetFarmPlot.State)
                {
                    case FarmPlotState.Arid:
                        Debug.Log("This plot is arid, nothing can be planted here!");
                        break;
                    case FarmPlotState.Arable:
                        Debug.Log("This plot is arable, anything can be planted here!");
                        break;
                    case FarmPlotState.Growing:
                        Debug.Log("This plot is growing!");
                        break;
                }
                
                break;
            }
        }
        
        Debug.Log("I'm doing it! I'm really doing it!!");
    }
}