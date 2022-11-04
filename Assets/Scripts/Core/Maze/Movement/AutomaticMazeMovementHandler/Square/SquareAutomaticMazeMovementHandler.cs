public class SquareAutomaticMazeMovementHandler : AutomaticMazeMovementHandler
{
    public SquareAutomaticMazeMovementHandler(IMovable movable, IMazeMovementControl movementControl, MazeCell[,] mazeGrid, MazeCell currentCell) : base(movable, movementControl, mazeGrid, currentCell)
    {
    }

    protected override MazeCell GetNextCell(MazeCell previousCell, CellDirections nextCellDirection, MazeCell[,] mazeGrid)
    {
        IGridCoordinates nextCellCoordinates = previousCell.GridCoordinates.ConvertToGridCartesian() + SquareGridDirections.Directions[nextCellDirection];
        MazeCell currentCell = mazeGrid[nextCellCoordinates.X, nextCellCoordinates.Y];
        return currentCell;
    }
}
