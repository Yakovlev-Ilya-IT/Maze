public class HexGrid : Grid
{
    public HexGrid(int width, int height) : base(width, height)
    {
    }

    protected override CellData GetNewCell(IGridCoordinates coordinates, uint idCounter)
    {
        AxialGridCoordinates axialCoordinates = coordinates.ConvertToGridAxial();
        return new HexCellData(axialCoordinates, idCounter);
    }
}
