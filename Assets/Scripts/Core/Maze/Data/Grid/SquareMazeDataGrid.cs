public class SquareMazeDataGrid : MazeDataGrid
{
    public SquareMazeDataGrid(int width, int height) : base(width, height)
    {
    }

    protected override MazeCellData GetNewCell(IGridCoordinates coordinates, uint idCounter)
    {
        CartesianGridCoordinates cartesianCoordinates = coordinates.ConvertToGridCartesian();
        return new SquareMazeCellData(cartesianCoordinates, idCounter);
    }
}
