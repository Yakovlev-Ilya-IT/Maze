using System.Collections.Generic;
using UnityEngine;

public class RingSquareMazeForm : IMazeGridForm
{
    private int _outsideRadius;
    private int _insideRadius;

    private const int MinimalOutsideRadiusForRing = 2;
    
    public float XOffset => 0;

    public float YOffset => 0;

    public RingSquareMazeForm(int width, int height)
    {
        if (width > height)
            _outsideRadius = width / 2;
        else
            _outsideRadius = height / 2;

        _insideRadius = _outsideRadius / 2;
    }

    public List<IGridCoordinates> GenerateGridCoordinates()
    {
        List<IGridCoordinates> gridCoordinates = new List<IGridCoordinates>();

        for (int x = -_outsideRadius; x <= _outsideRadius; x++)
        {
            int yBoundOutside = Mathf.FloorToInt(GetY(x, _outsideRadius));
            int yBoundInside = Mathf.FloorToInt(GetY(x, _insideRadius));
            for (int y = -yBoundOutside; y <= yBoundOutside; y++)
            {
                if (CheckPossibilityCreatingRing(x, y, yBoundInside))
                    continue;

                gridCoordinates.Add(new CartesianGridCoordinates(x, y));
            }
        }
        return gridCoordinates;
    }

    private float GetY(int x, int radius) => Mathf.Sqrt((float)radius * radius - (float)x * x);

    private bool CheckPossibilityCreatingRing(int x, int y, int yBoundInside) => y >= -yBoundInside && y <= yBoundInside && Mathf.Abs(x) <= _insideRadius && _outsideRadius > MinimalOutsideRadiusForRing;
}
