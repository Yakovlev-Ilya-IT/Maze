public class SquareGrid : Grid
{
    public SquareGrid(int width, int height) : base(width, height)
    {
    }

    protected override CellData GetNewCell(IGridCoordinates coordinates, uint idCounter)
    {
        CartesianGridCoordinates cartesianCoordinates = coordinates.ConvertToGridCartesian();
        return new SquareCellData(cartesianCoordinates, idCounter);
    }
}
