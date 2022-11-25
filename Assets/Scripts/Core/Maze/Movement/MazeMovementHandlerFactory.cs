public abstract class MazeMovementHandlerFactory
{
    public abstract IMazeMovementHandler Get(MovementHandlerType type, IMovable movable, IMazeMovementControl movementControl, IMazeGrid mazeGrid, MazeCell currentCell);
}
