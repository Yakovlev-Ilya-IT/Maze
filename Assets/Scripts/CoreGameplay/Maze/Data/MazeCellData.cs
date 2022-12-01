using System.Collections.Generic;

public class MazeCellData
{
    private IGridCoordinates _coordinates;
    private uint _id;
    private Dictionary<CellDirections, IGridCoordinates> _directionToNeighboursCoordinates;

    public IGridCoordinates Coordinates => _coordinates;
    public uint Id => _id;
    public Dictionary<CellDirections, IGridCoordinates> DirectionToNeighboursCoordinates => _directionToNeighboursCoordinates;

    public MazeCellData(IGridCoordinates coordinates, uint id)
    {
        _coordinates = coordinates;
        _id = id;
        _directionToNeighboursCoordinates = new Dictionary<CellDirections, IGridCoordinates>();
    }

    public void AddNeighbourCoordinates(CellDirections direction, IGridCoordinates coordinates) => _directionToNeighboursCoordinates.Add(direction, coordinates);
}
