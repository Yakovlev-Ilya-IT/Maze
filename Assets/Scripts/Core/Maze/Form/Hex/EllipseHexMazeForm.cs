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

    public Dictionary<IGridCoordinates, MazeCellData> GenerateDataGrid()
    {
        Dictionary<IGridCoordinates, MazeCellData> gridData = new Dictionary<IGridCoordinates, MazeCellData>();
        uint id = 0;

        for (int q = -_a; q <= _a; q++)
        {
            int r1 = GetLowerBoundR(q);
            int r2 = GetUpperBoundR(q);
            for (int r = r1; r <= r2; r++)
            {
                AxialGridCoordinates coordinates = new AxialGridCoordinates(q, r);
                gridData.Add(coordinates, new HexMazeCellData(coordinates, id));
                id++;
            }
        }

        return gridData;
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
