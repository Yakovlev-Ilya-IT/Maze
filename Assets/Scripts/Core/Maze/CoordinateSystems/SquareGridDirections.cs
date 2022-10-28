using System.Collections.Generic;
using UnityEngine;

public static class SquareGridDirections
{
    public readonly static Dictionary<CellDirections, Vector2Int> Directions = new Dictionary<CellDirections, Vector2Int>()
    {
        {CellDirections.Up, Vector2Int.up},
        {CellDirections.Right, Vector2Int.right},
        {CellDirections.Down, Vector2Int.down},
        {CellDirections.Left, Vector2Int.left}
    };
}
