using System.Collections.Generic;
using UnityEngine;

public class RectangleHexMazeForm : IMazeGridForm
{
    private int _width;
    private int _height;

    public float XOffset => _width / 2f;
    public float YOffset => _height / 2f - 0.5f;

    public RectangleHexMazeForm(int width, int height)
    {
        _width = width;
        _height = height;
    }

    public Dictionary<IGridCoordinates, MazeCellData> GenerateDataGrid()
    {
        Dictionary<IGridCoordinates, MazeCellData> gridData = new Dictionary<IGridCoordinates, MazeCellData>();
        uint id = 0;

        for (int r = 0; r < _height; r++)
        {
            int rOffset = Mathf.CeilToInt(r / 2f);
            for (int q = 0 - rOffset; q < _width - rOffset; q++)
            {
                AxialGridCoordinates coordinates = new AxialGridCoordinates(q, r);
                gridData.Add(coordinates, new HexMazeCellData(coordinates, id));
                id++;
            }
        }

        return gridData;
    }
}
