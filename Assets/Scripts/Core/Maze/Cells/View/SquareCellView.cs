using System.Collections.Generic;
using UnityEngine;

public class SquareCellView : CellView
{
    [SerializeField] private CellWallView _leftWall;
    [SerializeField] private CellWallView _upWall;
    [SerializeField] private CellWallView _rightWall;
    [SerializeField] private CellWallView _downWall;

    public override void Initialize(CellData data)
    {
        base.Initialize(data);

        _walls = new Dictionary<CellDirections, CellWallView>
        {
            {CellDirections.Up, _upWall},
            {CellDirections.Right, _rightWall},
            {CellDirections.Down, _downWall},
            {CellDirections.Left, _leftWall}
        };

        SetupWalls(data);
    }
}
