using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmSpriteReference
{
    public GameObject SeedObject;
    public Vector3Int PlotLocation;
    public PlantType PlantType;
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

        // Add reference to plant sprites
        PlantSprites.Add(new FarmSpriteReference { SeedObject = seedReference, PlotLocation = farmPlotCoordinate, PlantType = targetFarmPlot.PlantType.Value });
    }

    public void GrowSeed(Vector3Int farmPlotCoordinate)
    {
        // Find and update target farm plot
        FarmPlot targetFarmPlot = FarmPlots.First(plot => plot.Location == farmPlotCoordinate);

        // Change farm plot state to grown
        targetFarmPlot.State = FarmPlotState.Grown;

        // Remove old seed sprite, replace with grown seed sprite
        GameObject oldSprite = PlantSprites.First(s => s.PlotLocation == farmPlotCoordinate).SeedObject;
        Destroy(oldSprite);

        // Remove reference to old plant sprite
        int i = 0;
        foreach (FarmSpriteReference farmReference in PlantSprites)
        {
            if (farmReference.PlotLocation == farmPlotCoordinate)
                break;
            i++;
        }
        PlantSprites.RemoveAt(i);

        // Caclulate world position from tile map coordinate
        Vector3 centerOfTile = Tilemap.GetCellCenterWorld(farmPlotCoordinate);

        // Place seed sprite at center of farm tile
        GameObject seedReference = (GameObject) Instantiate(growingSeedPrefab, centerOfTile, Quaternion.identity);

        // Add reference to plant sprites
        PlantSprites.Add(new FarmSpriteReference { SeedObject = seedReference, PlotLocation = farmPlotCoordinate, PlantType = targetFarmPlot.PlantType.Value });
    }

    public void HarvestPlant(Vector3Int farmPlotCoordinate)
    {
        // Find and update target farm plot
        FarmPlot targetFarmPlot = FarmPlots.First(plot => plot.Location == farmPlotCoordinate);

        // Change farm plot state to arable, allow for new seeds to be planted here
        targetFarmPlot.State = FarmPlotState.Arable;
        
        // Remove old seed sprite, replace with grown seed sprite
        GameObject oldSprite = PlantSprites.First(s => s.PlotLocation == farmPlotCoordinate).SeedObject;
        Destroy(oldSprite);

        // Update reference to plant sprites
        int i = 0;
        foreach (FarmSpriteReference farmReference in PlantSprites)
        {
            if (farmReference.PlotLocation == farmPlotCoordinate)
                break;
            i++;
        }
        PlantSprites.RemoveAt(i);

        // TODO: Add plant to inventory
    }
}