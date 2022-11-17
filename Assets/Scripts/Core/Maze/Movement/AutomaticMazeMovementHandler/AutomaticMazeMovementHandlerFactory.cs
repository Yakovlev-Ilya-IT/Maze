using UnityEngine;

public class AutomaticMazeMovementHandlerFactory : MazeMovementHandlerFactory
{
    public override IMazeMovementHandler Get(MazeCellType type, IMovable movable, IMazeMovementControl movementControl, IMazeGrid mazeGrid, MazeCell currentCell)
    {
        switch (type)
        {
            case MazeCellType.Square:
                return new SquareAutomaticMazeMovementHandler(movable, movementControl, mazeGrid, currentCell);
            case MazeCellType.Hex:
                return new HexAutomaticMazeMovementHandler(movable, movementControl, mazeGrid, currentCell);
            default:
                break;
        }

        Debug.LogError($"No handler for {type}");
        return new SquareAutomaticMazeMovementHandler(movable, movementControl, mazeGrid, currentCell);
    }
}
