public class HexAutomaticMazeMovementHandler : AutomaticMazeMovementHandler
{
    public HexAutomaticMazeMovementHandler(IMovable movable, IMazeMovementControl movementControl, IMazeGrid mazeGrid, MazeCell currentCell) : base(movable, movementControl, mazeGrid, currentCell)
    {
    }

    protected override MazeCell GetNextCell(MazeCell previousCell, CellDirections nextCellDirection, IMazeGrid mazeGrid)
    {
        IGridCoordinates nextCellCoordinates = previousCell.GridCoordinates.ConvertToGridAxial() + HexGridDirections.Directions[nextCellDirection];
        mazeGrid.TryGetCell(nextCellCoordinates, out MazeCell currentCell);
        return currentCell;
    }
}
