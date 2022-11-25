using System.Collections.Generic;

public class RectangleSquareMazeForm : IMazeGridForm
{
    private int _width;
    private int _height;

    public float XOffset => _width / 2f - 0.5f;
    public float YOffset => _height / 2f - 0.5f;

    public RectangleSquareMazeForm(int width, int height)
    {
        _width = width;
        _height = height;
    }

    public Dictionary<IGridCoordinates, MazeCellData> GenerateDataGrid()
    {
        Dictionary<IGridCoordinates, MazeCellData> gridData = new Dictionary<IGridCoordinates, MazeCellData>();
        uint id = 0;

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                CartesianGridCoordinates coordinates = new CartesianGridCoordinates(x, y);
                gridData.Add(coordinates, new SquareMazeCellData(coordinates, id));
                id++;
            }
        }

        return gridData;
    }
}