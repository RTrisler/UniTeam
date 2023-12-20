using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class MapTileManager : MonoBehaviour
{
    // Public fields
    public Vector3Int PlayerCurrentTile;
    public List<Vector3Int> TileGridCoordinates;
    //  Note: First element in list = [-18,-9] (bottomleftmost tile), last element = [18,8] (upperrightmost tile)
    //          => X range: -18 -> 8, Y range: -9 -> 8, 0 inclusive

    // Inspector fields
    public Tilemap Tilemap;
    
    [SerializeField]
    Transform playerTransform;

    void Start()
    {
    }

    void Update()
    {
        PlayerCurrentTile = Tilemap.WorldToCell(playerTransform.position);
        Debug.Log("Current tile: " + PlayerCurrentTile);
    }
}
