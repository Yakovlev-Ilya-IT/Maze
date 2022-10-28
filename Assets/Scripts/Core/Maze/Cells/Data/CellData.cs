using System.Collections.Generic;

public abstract class CellData
{
    private IGridCoordinates _coordinates;

    private uint _id;

    public IGridCoordinates Coordinates => _coordinates;
    public uint Id => _id;
    public abstract Dictionary<CellDirections, bool> Walls { get; }

    public CellData(uint id, IGridCoordinates coordinates)
    {
        _coordinates = coordinates;
        _id = id;
    }
}
