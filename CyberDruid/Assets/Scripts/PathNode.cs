using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
   private Grid<PathNode> grid;
   public int x, y;

    public int gCost;
    public int hCost;
    public int fCost;
    public bool isWalkable;

    public PathNode cameFromNode;

   public PathNode(Grid<PathNode> grid, int x, int y)
   {
        this.grid = grid;
        this.x = x;
        this.y = y;
   }

   public void CalculateFCost()
   {
        fCost = gCost + hCost;
   }

    public void SetIsWalkable(bool isWalkable) {
        this.isWalkable = isWalkable;
        grid.TriggerGridObjectChanged(x, y);
    }


    public override string ToString(){
        return x + ", " + y;
    }
}
