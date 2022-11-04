using System.Collections.Generic;
using UnityEngine;

public class SquareMazeCellView : MazeCellView
{
    [SerializeField] private CellWallView _leftWall;
    [SerializeField] private CellWallView _upWall;
    [SerializeField] private CellWallView _rightWall;
    [SerializeField] private CellWallView _downWall;

    protected override Dictionary<CellDirections, CellWallView> GetWalls()
    {
        return new Dictionary<CellDirections, CellWallView>
        {
            {CellDirections.Up, _upWall},
            {CellDirections.Right, _rightWall},
            {CellDirections.Down, _downWall},
            {CellDirections.Left, _leftWall}
        };
    }
}