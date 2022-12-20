using System.Collections.Generic;
using UnityEngine;

public class EllipseSquareMazeForm : IMazeGridForm
{
    private int _a;
    private int _b;

    public float XOffset => 0;
    public float YOffset => 0;

    public Quaternion Rotation => Quaternion.Euler(new Vector3(0, 45, 0));

    private void Initialize(int a, int b)
    {
        if (a > b)
        {
            _a = a / 2;
            _b = b / 2;
        }
        else
        {
            _a = b / 2;
            _b = a / 2;
        }
    }

    public List<IGridCoordinates> GenerateGridCoordinates(int a, int b)
    {
        Initialize(a, b);

        List<IGridCoordinates> gridCoordinates = new List<IGridCoordinates>();

        for (int x = -_a; x <= _a; x++)
        {
            int yBound = Mathf.FloorToInt(GetY(x));
            for (int y = -yBound; y <= yBound; y++)
            {
                gridCoordinates.Add(new CartesianGridCoordinates(x, y));
            }
        }
        return gridCoordinates;
    }

    private float GetY(int x) => Mathf.Sqrt((1f - (float)x * x / (_a * _a)) * _b * _b);
}
