using System.Collections.Generic;
using UnityEngine;

public class ParallelogramSquareMazeForm : IMazeGridForm
{
    private int _width;
    private int _height;

    public float XOffset => (_width + (_height - 1) / 2f) / 2f - 0.5f;
    public float YOffset => _height / 2f - 0.5f;

    public ParallelogramSquareMazeForm(int width, int height)
    {
        _width = width;
        _height = height;
    }

    public Dictionary<IGridCoordinates, MazeCellData> GenerateDataGrid()
    {
        Dictionary<IGridCoordinates, MazeCellData> gridData = new Dictionary<IGridCoordinates, MazeCellData>();
        uint id = 0;

        for (int y = 0; y < _height; y++)
        {
            for (int x = y/2; x < _width + y/2; x++)
            {
                CartesianGridCoordinates coordinates = new CartesianGridCoordinates(x, y);
                gridData.Add(coordinates, new SquareMazeCellData(coordinates, id));
                id++;
            }
        }

        return gridData;
    }
}
