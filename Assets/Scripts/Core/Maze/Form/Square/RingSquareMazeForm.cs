using System.Collections.Generic;
using UnityEngine;

public class RingSquareMazeForm : IMazeGridForm
{
    private int _outsideRadius;
    private int _insideRadius;

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

    public Dictionary<IGridCoordinates, MazeCellData> GenerateDataGrid()
    {
        Dictionary<IGridCoordinates, MazeCellData> gridData = new Dictionary<IGridCoordinates, MazeCellData>();
        uint id = 0;

        for (int x = -_outsideRadius; x <= _outsideRadius; x++)
        {
            int yBoundOutside = Mathf.FloorToInt(GetY(x, _outsideRadius));
            int yBoundInside = Mathf.FloorToInt(GetY(x, _insideRadius));
            for (int y = -yBoundOutside; y <= yBoundOutside; y++)
            {
                if (y >= -yBoundInside && y <= yBoundInside && Mathf.Abs(x) <= _insideRadius)
                    continue;

                CartesianGridCoordinates coordinates = new CartesianGridCoordinates(x, y);
                gridData.Add(coordinates, new SquareMazeCellData(coordinates, id));
                id++;
            }
        }
        return gridData;
    }

    private float GetY(int x, int radius) => Mathf.Sqrt((float)radius * radius - (float)x * x);
}
