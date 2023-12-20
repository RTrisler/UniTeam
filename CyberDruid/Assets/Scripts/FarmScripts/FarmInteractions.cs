using System.Collections;
using System.Collections.Generic;
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
        
        foreach (Vector3Int coordinate in TestFarmCoordinates)
        {
            if (MapTileManager.PlayerCurrentTile == coordinate)
            {
                Debug.Log("Pressed Q, standing on: " + coordinate);
                // Player is standing on the target farm plot, and the farm plot state is appropriate
                //targetFarmPlot = farmPlot;
                break;   
            }
        }
        
        Debug.Log("I'm doing it! I'm really doing it!!");
    }
}
