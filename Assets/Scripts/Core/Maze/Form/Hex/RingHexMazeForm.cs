using System.Collections.Generic;
using UnityEngine;

public class RingHexMazeForm : IMazeGridForm
{
    private int _outsideRadius;
    private int _insideRadius;

    public float XOffset => 0.5f;

    public float YOffset => 0;

    public RingHexMazeForm(int width, int height)
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

        for (int q = -_outsideRadius; q <= _outsideRadius; q++)
        {

            int r1Outside = GetLowerBoundR(q, _outsideRadius);
            int r2Outside = GetUpperBoundR(q, _outsideRadius);

            int r1Inside = GetLowerBoundR(q, _insideRadius);
            int r2Inside = GetUpperBoundR(q, _insideRadius);

            for (int r = r1Outside; r <= r2Outside; r++)
            {
                if (r >= r1Inside && r <= r2Inside && Mathf.Abs(q) <= _insideRadius)
                    continue;

                AxialGridCoordinates coordinates = new AxialGridCoordinates(q, r);
                gridData.Add(coordinates, new HexMazeCellData(coordinates, id));
                id++;
            }
        }

        return gridData;
    }

    private int GetLowerBoundR(int q, int radius)
    {
        return Mathf.Max(-radius, -q - radius);
    }

    private int GetUpperBoundR(int q, int radius)
    {
        return Mathf.Min(radius, -q + radius);
    }
}
