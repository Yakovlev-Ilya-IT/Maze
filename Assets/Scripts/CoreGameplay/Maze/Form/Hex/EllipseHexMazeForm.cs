using System.Collections.Generic;
using UnityEngine;

public class EllipseHexMazeForm : IMazeGridForm
{
    private int _a;
    private int _b;

    public float XOffset => 0.5f;
    public float YOffset => 0;

    public EllipseHexMazeForm(int a, int b)
    {
        if(a > b)
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

    public List<IGridCoordinates> GenerateGridCoordinates()
    {
        List<IGridCoordinates> gridCoordinates = new List<IGridCoordinates>();

        for (int q = -_a; q <= _a; q++)
        {
            int r1 = GetLowerBoundR(q);
            int r2 = GetUpperBoundR(q);
            for (int r = r1; r <= r2; r++)
            {
                gridCoordinates.Add(new AxialGridCoordinates(q, r));
            }
        }

        return gridCoordinates;
    }

    private int GetLowerBoundR(int q)
    {
        return Mathf.Max(-_a, -q - _b);
    }

    private int GetUpperBoundR(int q)
    {
        return Mathf.Min(_a, -q + _b);
    }
}
