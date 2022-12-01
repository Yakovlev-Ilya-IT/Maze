using System.Collections.Generic;
using UnityEngine;

public class HexMovementControlDisplay : MovementControlDisplay
{
    [SerializeField] private DirectionArrow _upLeftDirectionArrow;
    [SerializeField] private DirectionArrow _upRightDirectionArrow;
    [SerializeField] private DirectionArrow _rightDirectionArrow;
    [SerializeField] private DirectionArrow _downRightDirectionArrow;
    [SerializeField] private DirectionArrow _downLeftDirectionArrowl;
    [SerializeField] private DirectionArrow _leftDirectionArrow;

    protected override Dictionary<CellDirections, DirectionArrow> GetDirectionArrows()
    {
        return new Dictionary<CellDirections, DirectionArrow>
        {
            {CellDirections.UpLeft, _upLeftDirectionArrow},
            {CellDirections.UpRight, _upRightDirectionArrow},
            {CellDirections.Right, _rightDirectionArrow},
            {CellDirections.DownRight, _downRightDirectionArrow},
            {CellDirections.DownLeft, _downLeftDirectionArrowl},
            {CellDirections.Left, _leftDirectionArrow}
        };
    }
}
