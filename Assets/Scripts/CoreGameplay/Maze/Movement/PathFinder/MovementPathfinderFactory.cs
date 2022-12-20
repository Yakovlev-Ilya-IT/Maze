using System;

public class MovementPathfinderFactory 
{
    public IMovementPathfinder Get(MovementPathfinderType type, IMazeGrid mazeGrid)
    {
        switch (type)
        {
            case MovementPathfinderType.UntilNextFork:
                return new PathfinderUntilNextFork(mazeGrid);
        }

        throw new ArgumentException($"No movement handler for {type}");
    }
}
