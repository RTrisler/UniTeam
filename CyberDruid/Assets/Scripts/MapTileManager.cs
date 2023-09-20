using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class MapTileManager : MonoBehaviour
{
    // Public fields
    public Tilemap Tilemap;
    public Vector3Int playerCurrentTile;
    

    public List<Vector3Int> TileGridCoordinates;
    //  Note: int[] in TileGridCoordinates takes the format [x,y], 
    //          where x and y are the coordinates of a tile within the tilemap 
    //  Note: First element in list = [-18,-9] (bottomleftmost tile), last element = [18,8] (upperrightmost tile)
    //          => X range: -18 -> 8, Y range: -9 -> 8, 0 inclusive

    // Inspector fields
    [SerializeField]
    Transform playerTransform;

    void Start()
    {
        TileGridCoordinates = new List<Vector3Int>();
        for (int n = Tilemap.cellBounds.xMin; n < Tilemap.cellBounds.xMax; n++)
        {
            for (int p = Tilemap.cellBounds.yMin; p < Tilemap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)Tilemap.transform.position.y));
                //Vector3 place = Tilemap.CellToWorld(localPlace);
                if (Tilemap.HasTile(localPlace))
                {
                    TileGridCoordinates.Add(localPlace);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerCurrentTile = Tilemap.WorldToCell(playerTransform.position);
        //Debug.Log("Player current tile: (" + playerCurrentTile.x + ", " + playerCurrentTile.y + ")");
    }
}
