using System.Collections.Generic;

public class SquareCellData : CellData
{
    private Dictionary<CellDirections, bool> _walls;

    public override Dictionary<CellDirections, bool> Walls => _walls;

    public SquareCellData(CartesianGridCoordinates coordinates, uint id) : base(id, coordinates)
    {
        _walls = new Dictionary<CellDirections, bool>
        {
            {CellDirections.Up, true},
            {CellDirections.Right, true},
            {CellDirections.Down, true},
            {CellDirections.Left, true}
        };
    }
}
