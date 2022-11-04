public abstract class MazeMovementHandlerFactory
{
    public abstract IMazeMovementHandler Get(MazeCellType type, IMovable movable, IMazeMovementControl movementControl, MazeCell[,] mazeGrid, MazeCell currentCell);
}
