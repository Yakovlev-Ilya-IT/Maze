using System.Collections.Generic;
using UnityEngine;

public class SquareMovementControlDisplay : MovementControlDisplay
{
    [SerializeField] private DirectionArrow _leftDirectionArrow;
    [SerializeField] private DirectionArrow _upDirectionArrow;
    [SerializeField] private DirectionArrow _rightDirectionArrow;
    [SerializeField] private DirectionArrow _downDirectionArrow;

    protected override Dictionary<CellDirections, DirectionArrow> GetDirectionArrows()
    {
        return new Dictionary<CellDirections, DirectionArrow>
        {
            {CellDirections.Up, _upDirectionArrow},
            {CellDirections.Right, _rightDirectionArrow},
            {CellDirections.Down, _downDirectionArrow},
            {CellDirections.Left, _leftDirectionArrow}
        };
    }
}
