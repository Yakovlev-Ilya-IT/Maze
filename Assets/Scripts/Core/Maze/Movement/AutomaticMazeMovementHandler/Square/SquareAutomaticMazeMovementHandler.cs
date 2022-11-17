public class SquareAutomaticMazeMovementHandler : AutomaticMazeMovementHandler
{
    public SquareAutomaticMazeMovementHandler(IMovable movable, IMazeMovementControl movementControl, IMazeGrid mazeGrid, MazeCell currentCell) : base(movable, movementControl, mazeGrid, currentCell)
    {
    }

    protected override MazeCell GetNextCell(MazeCell previousCell, CellDirections nextCellDirection, IMazeGrid mazeGrid)
    {
        IGridCoordinates nextCellCoordinates = previousCell.GridCoordinates.ConvertToGridCartesian() + SquareGridDirections.Directions[nextCellDirection];
        mazeGrid.TryGetCell(nextCellCoordinates, out MazeCell currentCell);
        return currentCell;
    }
}
