public class HexMazeDataGrid : MazeDataGrid
{
    public HexMazeDataGrid(int width, int height) : base(width, height)
    {
    }

    protected override MazeCellData GetNewCell(IGridCoordinates coordinates, uint idCounter)
    {
        AxialGridCoordinates axialCoordinates = coordinates.ConvertToGridAxial();
        return new HexMazeCellData(axialCoordinates, idCounter);
    }
}
