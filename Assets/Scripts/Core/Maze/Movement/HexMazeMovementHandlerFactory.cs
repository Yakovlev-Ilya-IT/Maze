using System;

public class HexMazeMovementHandlerFactory : MazeMovementHandlerFactory
{
    public override IMazeMovementHandler Get(MovementHandlerType type, IMovable movable, IMazeMovementControl movementControl, IMazeGrid mazeGrid, MazeCell currentCell)
    {
        switch (type)
        {
            case MovementHandlerType.Automatic:
                return new HexAutomaticMazeMovementHandler(movable, movementControl, mazeGrid, currentCell);
        }

        throw new ArgumentException($"No movement handler for {type}");
    }
}
