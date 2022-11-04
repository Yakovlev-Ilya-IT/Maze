using System.Collections.Generic;

public abstract class MazeCellData
{
    private IGridCoordinates _coordinates;

    private uint _id;

    public IGridCoordinates Coordinates => _coordinates;
    public uint Id => _id;
    public abstract Dictionary<CellDirections, bool> Walls { get; }

    public MazeCellData(uint id, IGridCoordinates coordinates)
    {
        _coordinates = coordinates;
        _id = id;
    }
}
