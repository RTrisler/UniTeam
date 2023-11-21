# Procedurally Generated Dungeon

The procedural dungeon generation uses a random walk and binary space partitioning algorithms

## Algorithms

### Random Walk (Drunkard’s Walk)

To understand the  Random Walk algorithm you can imagine we have a grid an agent at a specific starting position. We make the agent walk one cell in a random direction and define how many steps the agent should take. We can then iterate this process to create a island like dungeon. We can also provide a new start position for the each iteration based off the already populated cells which will allow us to create much larger rooms.

## Implementation

In the scene we will have an object in the hierarchy name CorridorFirstDungeonGenerator with the CorridorFirstDungeonGenerator script component. The following outline all the scripts related to the dungeon

### ProceduralGenerationAlgorithms.cs

The ProceduralGenerationAlgorithms is a public static class that holds the both the simple random walk algorithm and the corridor first algorithm. The simple random walk  method returns a HashSet<Vector2Int> and takes in a Vector2Int start position and an integer walkLength. A HashSet is a collection that allows us to store unique values if the type that we store in it implements the Equals and GetHashCode methods. If you view the definition of Vector2Int you will see that it has both of these methods. This means we can use it inside our HashSetted data which will easily allow us to remove duplicates since Random Walk does not prevent the agent from going over a previously visited cell. 

In the method we first create a new HashSet to store our path positions and add the start position to the path as well as setting the previous position variable to the start position. Then we start a loop from 0 to the walk length and get a new position in a random cardinal direction by calling our method in the Direction2D class implemented within the file, and we add that to the previous position then update the previous position.

```csharp
HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startPosition);
        var previousPosition = startPosition;

        for (int i = 0; i < walkLength; i++)
        {
            var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }
        return path;
```

### AbstractDungeonGenerator.cs

AbstractDungeonGenerator is the abstract class for the dungeons

### CorridorFirstDungeonGenerator.cs