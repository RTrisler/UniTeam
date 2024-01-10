using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmSpriteReference
{
    public GameObject seedObject;
    public Vector3Int plotLocation;
}

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
    public GameObject seedPrefab;
    public GameObject growingSeedPrefab;

    [SerializeField]
    Transform playerTransform;

    // Private fields
    List<FarmSpriteReference> PlantSprites;

    void Start()
    {
        FarmPlots = new List<FarmPlot>();
        PlantSprites = new List<FarmSpriteReference>();
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
    }

    public void PlantSeed(Vector3Int farmPlotCoordinate)
    {
        // Find and update target farm plot
        FarmPlot targetFarmPlot = FarmPlots.First(plot => plot.Location == farmPlotCoordinate);

        // Set farm plot plant to seed type (TODO: specify this when we have more seeds)
        targetFarmPlot.PlantType = PlantType.FungalBlade;

        // Change farm plot state to growing
        targetFarmPlot.State = FarmPlotState.Growing;

        // Caclulate world position from tile map coordinate
        Vector3 centerOfTile = Tilemap.GetCellCenterWorld(farmPlotCoordinate);

        // Place seed sprite at center of farm tile
        GameObject seedReference = (GameObject) Instantiate(seedPrefab, centerOfTile, Quaternion.identity);

        PlantSprites.Add(new FarmSpriteReference { seedObject = seedReference, plotLocation = farmPlotCoordinate });
    }

    public void GrowSeed(Vector3Int farmPlotCoordinate)
    {
        // Find and update target farm plot
        FarmPlot targetFarmPlot = FarmPlots.First(plot => plot.Location == farmPlotCoordinate);

        // Change farm plot state to grown
        targetFarmPlot.State = FarmPlotState.Grown;

        // Caclulate world position from tile map coordinate
        Vector3 centerOfTile = Tilemap.GetCellCenterWorld(farmPlotCoordinate);

        // Remove old seed sprite, replace with grown seed sprite
        GameObject oldSprite = PlantSprites.First(s => s.plotLocation == farmPlotCoordinate).seedObject;
        Destroy(oldSprite);

        // Place seed sprite at center of farm tile
        Instantiate(growingSeedPrefab, centerOfTile, Quaternion.identity);
    }
}