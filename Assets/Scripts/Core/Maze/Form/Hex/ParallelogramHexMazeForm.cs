using System.Collections.Generic;
using UnityEngine;

public class ParallelogramHexMazeForm : IMazeGridForm
{
    private int _width;
    private int _height;

    public float XOffset => (_width + _height / Mathf.Tan(60 * Mathf.Deg2Rad)) / 2f - 0.5f;
    public float YOffset => _height / 2f - 0.5f;

    public ParallelogramHexMazeForm(int width, int height)
    {
        _width = width;
        _height = height;
    }

    public Dictionary<IGridCoordinates, MazeCellData> GenerateDataGrid()
    {
        Dictionary<IGridCoordinates, MazeCellData> gridData = new Dictionary<IGridCoordinates, MazeCellData>();
        uint id = 0;

        for (int q = 0; q < _width; q++)
        {
            for (int r = 0; r < _height; r++)
            {
                AxialGridCoordinates coordinates = new AxialGridCoordinates(q, r);
                gridData.Add(coordinates, new HexMazeCellData(coordinates, id));
                id++;
            }
        }

        return gridData;
    }
}
