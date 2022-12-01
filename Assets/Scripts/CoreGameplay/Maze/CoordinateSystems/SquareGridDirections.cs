using System.Collections.Generic;

public static class SquareGridDirections
{
    public readonly static Dictionary<CellDirections, CartesianGridCoordinates> Directions = new Dictionary<CellDirections, CartesianGridCoordinates>()
    {
        {CellDirections.Up, new CartesianGridCoordinates(0, 1)},
        {CellDirections.Right, new CartesianGridCoordinates(1, 0)},
        {CellDirections.Down, new CartesianGridCoordinates(0, -1)},
        {CellDirections.Left, new CartesianGridCoordinates(-1, 0)}
    };
}
