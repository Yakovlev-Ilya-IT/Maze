using System.Collections.Generic;
using UnityEngine;

public class HexMazeCellView : MazeCellView
{
    [SerializeField] private CellWallView _upLeftWall;
    [SerializeField] private CellWallView _upRightWall;
    [SerializeField] private CellWallView _rightWall;
    [SerializeField] private CellWallView _downRightWall;
    [SerializeField] private CellWallView _downLeftWall;
    [SerializeField] private CellWallView _leftWall;

    protected override Dictionary<CellDirections, CellWallView> GetWalls()
    {
        return new Dictionary<CellDirections, CellWallView>
        {
            {CellDirections.UpLeft, _upLeftWall},
            {CellDirections.UpRight, _upRightWall},
            {CellDirections.Right, _rightWall},
            {CellDirections.DownRight, _downRightWall},
            {CellDirections.DownLeft, _downLeftWall},
            {CellDirections.Left, _leftWall}
        };
    }
}
