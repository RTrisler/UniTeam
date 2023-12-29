using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class MapTileManager : MonoBehaviour
{
    // Public fields
    public Vector3Int PlayerCurrentTile;
    public List<Vector3Int> TileGridCoordinates;
    public List<FarmPlot> FarmPlots;


    // Inspector fields
    public Tilemap Tilemap;
    public Tile aridSoil;
    public Tile arableSoil;

    [SerializeField]
    Transform playerTransform;


    void Start()
    {
        FarmPlots = new List<FarmPlot>();
        // initialize farm with mix of arable and arid soil
        // Note: This will be pulled from saved player information when in game
        int i = 0;
        foreach (Vector3Int coordinate in TileGridCoordinates)
        {
            if (i%2 == 0)
            {
                Tilemap.SetTile(coordinate, aridSoil);
                FarmPlots.Add(new FarmPlot 
                {
                    State = FarmPlotState.Arid,
                    Location = coordinate,
                    PlantType = null
                });
            }
            else
            {
                Tilemap.SetTile(coordinate, arableSoil);
                FarmPlots.Add(new FarmPlot 
                {
                    State = FarmPlotState.Arable,
                    Location = coordinate,
                    PlantType = null
                });
            }

            i++;
        }
    }

    void Update()
    {
        PlayerCurrentTile = Tilemap.WorldToCell(playerTransform.position);
        Debug.Log("Current tile: " + PlayerCurrentTile);
    }
}