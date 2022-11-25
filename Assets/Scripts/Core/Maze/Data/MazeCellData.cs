using System.Collections.Generic;

public abstract class MazeCellData
{
    private IGridCoordinates _coordinates;
    private uint _id;
    private Dictionary<CellDirections, IGridCoordinates> _directionToNeighboursCoordinates;

    public IGridCoordinates Coordinates => _coordinates;
    public uint Id => _id;
    public abstract Dictionary<CellDirections, bool> Walls { get; }
    public Dictionary<CellDirections, IGridCoordinates> DirectionToNeighboursCoordinates => _directionToNeighboursCoordinates;

    public MazeCellData(uint id, IGridCoordinates coordinates)
    {
        _coordinates = coordinates;
        _id = id;
        _directionToNeighboursCoordinates = new Dictionary<CellDirections, IGridCoordinates>();
    }

    public void AddNeighbourCoordinates(CellDirections direction, IGridCoordinates coordinates) => _directionToNeighboursCoordinates.Add(direction, coordinates);
}
