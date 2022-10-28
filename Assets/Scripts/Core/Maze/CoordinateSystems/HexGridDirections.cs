using System.Collections.Generic;
using UnityEngine;

public static class HexGridDirections 
{
    public readonly static Dictionary<CellDirections, Vector2Int> Directions = new Dictionary<CellDirections, Vector2Int>()
    {
        {CellDirections.UpLeft, new Vector2Int(-1, 1)},
        {CellDirections.UpRight, new Vector2Int(0, 1)},
        {CellDirections.Right, new Vector2Int(1, 0)},
        {CellDirections.DownRight,  new Vector2Int(1, -1)},
        {CellDirections.DownLeft, new Vector2Int(0, -1)},
        {CellDirections.Left, new Vector2Int(-1, 0)}
    };
}
