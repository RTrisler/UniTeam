using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingHeatMap : MonoBehaviour {

    [SerializeField] private HeatMapVisual heatMapVisual;
    private Grid grid;

    private void Start() {
        grid = new Grid(150, 100, 2f, Vector3.zero);

        heatMapVisual.SetGrid(grid);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 position = GetMouseWorldPosition();
            grid.AddValue(position, 100, 2, 25);
        }
    }

     // Get Mouse Position in World with Z = 0f
    public static Vector3 GetMouseWorldPosition() {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    public static Vector3 GetMouseWorldPositionWithZ() {
            return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        }

    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera) {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera) {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

    /*public void AddValue(int x, int y, TGridObject value) {
        SetValue(x, y, GetValue(x, y) + value);
    }*/

    /*public void AddValue(Vector3 worldPosition, TGridObject value, int fullValueRange, int totalRange) {
        int lowerValueAmount = Mathf.RoundToInt((float)value / (totalRange - fullValueRange));

        GetXY(worldPosition, out int originX, out int originY);
        for (int x = 0; x < totalRange; x++) {
            for (int y = 0; y < totalRange - x; y++) {
                int radius = x + y;
                int addValueAmount = value;
                if (radius >= fullValueRange) {
                    addValueAmount -= lowerValueAmount * (radius - fullValueRange);
                }

                AddValue(originX + x, originY + y, addValueAmount);

                if (x != 0) {
                    AddValue(originX - x, originY + y, addValueAmount);
                }
                if (y != 0) {
                    AddValue(originX + x, originY - y, addValueAmount);
                    if (x != 0) {
                        AddValue(originX - x, originY - y, addValueAmount);
                    }
                }
            }
        }
    } */
}
