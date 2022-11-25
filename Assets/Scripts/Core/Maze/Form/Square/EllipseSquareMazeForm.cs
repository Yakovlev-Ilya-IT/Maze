using System.Collections.Generic;
using UnityEngine;

public class EllipseSquareMazeForm : IMazeGridForm
{
    private int _a;
    private int _b;

    public float XOffset => 0;
    public float YOffset => 0;

    public EllipseSquareMazeForm(int a, int b)
    {
        _a = a / 2;
        _b = b / 2;
    }

    public Dictionary<IGridCoordinates, MazeCellData> GenerateDataGrid()
    {
        Dictionary<IGridCoordinates, MazeCellData> gridData = new Dictionary<IGridCoordinates, MazeCellData>();
        uint id = 0;

        for (int x = -_a; x <= _a; x++)
        {
            int yBound = Mathf.FloorToInt(GetY(x));
            for (int y = -yBound; y <= yBound; y++)
            {
                CartesianGridCoordinates coordinates = new CartesianGridCoordinates(x, y);
                gridData.Add(coordinates, new SquareMazeCellData(coordinates, id));
                id++;
            }
        }
        return gridData;
    }

    private float GetY(int x) => Mathf.Sqrt((1f - (float)x * x / (_a * _a)) * _b * _b);
}
