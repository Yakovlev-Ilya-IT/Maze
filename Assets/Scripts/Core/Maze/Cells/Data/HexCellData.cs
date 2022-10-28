using System.Collections.Generic;

public class HexCellData : CellData
{
    private Dictionary<CellDirections, bool> _walls;

    public override Dictionary<CellDirections, bool> Walls => _walls;

    public HexCellData(AxialGridCoordinates coordinates, uint id) : base(id, coordinates)
    {
        _walls = new Dictionary<CellDirections, bool>
        {
            {CellDirections.UpLeft, true},
            {CellDirections.UpRight, true},
            {CellDirections.Right, true},
            {CellDirections.DownRight, true},
            {CellDirections.DownLeft, true},
            {CellDirections.Left, true}
        };
    }
}
