public class HexAutomaticMazeMovementHandler : AutomaticMazeMovementHandler
{
    public HexAutomaticMazeMovementHandler(IMovable movable, IMazeMovementControl movementControl, MazeCell[,] mazeGrid, MazeCell currentCell) : base(movable, movementControl, mazeGrid, currentCell)
    {
    }

    protected override MazeCell GetNextCell(MazeCell previousCell, CellDirections nextCellDirection, MazeCell[,] mazeGrid)
    {
        IGridCoordinates nextCellCoordinates = previousCell.GridCoordinates.ConvertToGridAxial() + HexGridDirections.Directions[nextCellDirection];
        MazeCell currentCell = mazeGrid[nextCellCoordinates.ConvertToGridCartesian().X, nextCellCoordinates.ConvertToGridCartesian().Y];
        return currentCell;
    }
}
