using System;

public class SquareMazeMovementHandlerFactory : MazeMovementHandlerFactory
{
    public override IMazeMovementHandler Get(MovementHandlerType type, IMovable movable, IMazeMovementControl movementControl, IMazeGrid mazeGrid, MazeCell currentCell)
    {
        switch (type)
        {
            case MovementHandlerType.Automatic:
                return new SquareAutomaticMazeMovementHandler(movable, movementControl, mazeGrid, currentCell);
        }

        throw new ArgumentException($"No movement handler for {type}");
    }
}
