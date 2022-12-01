using System;

public class MazeMovementHandlerFactory
{
    public IMazeMovementHandler Get(MovementHandlerType type, IMovable movable, IMazeMovementControl movementControl, IMazeGrid mazeGrid, MazeCell currentCell)
    {
        switch (type)
        {
            case MovementHandlerType.Automatic:
                return new AutomaticMazeMovementHandler(movable, movementControl, mazeGrid, currentCell);
        }

        throw new ArgumentException($"No movement handler for {type}");
    }
}
