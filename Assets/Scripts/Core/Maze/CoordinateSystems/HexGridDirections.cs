using System.Collections.Generic;

public static class HexGridDirections 
{
    public readonly static Dictionary<CellDirections, AxialGridCoordinates> Directions = new Dictionary<CellDirections, AxialGridCoordinates>()
    {
        {CellDirections.UpLeft, new AxialGridCoordinates(-1, 1)},
        {CellDirections.UpRight, new AxialGridCoordinates(0, 1)},
        {CellDirections.Right, new AxialGridCoordinates(1, 0)},
        {CellDirections.DownRight,  new AxialGridCoordinates(1, -1)},
        {CellDirections.DownLeft, new AxialGridCoordinates(0, -1)},
        {CellDirections.Left, new AxialGridCoordinates(-1, 0)}
    };
}
