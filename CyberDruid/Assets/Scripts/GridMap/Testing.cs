﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Testing : MonoBehaviour {

    [SerializeField] private HeatMapVisual heatMapVisual;
    [SerializeField] private HeatMapBoolVisual heatMapBoolVisual;
    [SerializeField] private HeatMapGenericVisual heatMapGenericVisual;
    private Grid<HeatMapGridObject> grid;
    private Grid<bool> boolGrid;
    private Grid<StringGridObject> stringGrid;

    private void Start() {
        grid = new Grid<HeatMapGridObject>(20, 10, 8f, Vector3.zero, (Grid<HeatMapGridObject> g, int x, int y) => new HeatMapGridObject(g, x, y));
        //boolGrid = new Grid<bool>(20, 10, 8f, Vector3.zero, (Grid<bool> g, int x, int y) => false);
        //stringGrid = new Grid<StringGridObject>(20, 10, 8f, Vector3.zero, (Grid<StringGridObject> g, int x, int y) => new StringGridObject(g, x, y));

        //heatMapVisual.SetGrid(grid);
        //heatMapBoolVisual.SetGrid(boolGrid);
        heatMapGenericVisual.SetGrid(grid);
    }

    private void Update() {
        Vector3 position = GetMouseWorldPosition();

        if (Input.GetMouseButtonDown(0)) {
            //boolGrid.SetGridObject(position, true);
            HeatMapGridObject heatMapGridObject = grid.GetGridObject(position);
            if (heatMapGridObject != null) {
                heatMapGridObject.AddValue(5);
            }
        }

        /*
        if (Input.GetKeyDown(KeyCode.A)) { stringGrid.GetGridObject(position).AddLetter("A"); }
        if (Input.GetKeyDown(KeyCode.B)) { stringGrid.GetGridObject(position).AddLetter("B"); }
        if (Input.GetKeyDown(KeyCode.C)) { stringGrid.GetGridObject(position).AddLetter("C"); }

        if (Input.GetKeyDown(KeyCode.Alpha1)) { stringGrid.GetGridObject(position).AddNumber("1"); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { stringGrid.GetGridObject(position).AddNumber("2"); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { stringGrid.GetGridObject(position).AddNumber("3"); }
        */
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

}

public class HeatMapGridObject {

    private const int MIN = 0;
    private const int MAX = 100;

    private Grid<HeatMapGridObject> grid;
    private int x;
    private int y;
    private int value;

    public HeatMapGridObject(Grid<HeatMapGridObject> grid, int x, int y) {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public void AddValue(int addValue) {
        value += addValue;
        value = Mathf.Clamp(value, MIN, MAX);
        grid.TriggerGridObjectChanged(x, y);
    }

    public float GetValueNormalized() {
        return (float)value / MAX;
    }

    public override string ToString() {
        return value.ToString();
    }

}

public class StringGridObject {
    
    private Grid<StringGridObject> grid;
    private int x;
    private int y;

    private string letters;
    private string numbers;
    
    public StringGridObject(Grid<StringGridObject> grid, int x, int y) {
        this.grid = grid;
        this.x = x;
        this.y = y;
        letters = "";
        numbers = "";
    }

    public void AddLetter(string letter) {
        letters += letter;
        grid.TriggerGridObjectChanged(x, y);
    }

    public void AddNumber(string number) {
        numbers += number;
        grid.TriggerGridObjectChanged(x, y);
    }

    public override string ToString() {
        return letters + "\n" + numbers;
    }
}
