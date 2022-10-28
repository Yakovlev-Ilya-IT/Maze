using System.Collections.Generic;
using UnityEngine;

public class HexCellView : CellView
{
    [SerializeField] private CellWallView _upLeftWall;
    [SerializeField] private CellWallView _upRightWall;
    [SerializeField] private CellWallView _rightWall;
    [SerializeField] private CellWallView _downRightWall;
    [SerializeField] private CellWallView _downLeftWall;
    [SerializeField] private CellWallView _leftWall;

    public override void Initialize(CellData data)
    {
        base.Initialize(data);

        _walls = new Dictionary<CellDirections, CellWallView>
        {
            {CellDirections.UpLeft, _upLeftWall},
            {CellDirections.UpRight, _upRightWall},
            {CellDirections.Right, _rightWall},
            {CellDirections.DownRight, _downRightWall},
            {CellDirections.DownLeft, _downLeftWall},
            {CellDirections.Left, _leftWall}
        };

        SetupWalls(data);
    }
}
