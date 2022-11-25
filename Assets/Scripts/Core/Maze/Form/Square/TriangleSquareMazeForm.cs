using System.Collections.Generic;

public class TriangleSquareMazeForm : IMazeGridForm
{
    private int _size;

    public float XOffset => (_size + _size / 2f) / 3f - 0.5f;
    public float YOffset => _size / 3f;

    public TriangleSquareMazeForm(int width, int height)
    {
        if (width > height)
            _size = width;
        else
            _size = height;
    }

    public Dictionary<IGridCoordinates, MazeCellData> GenerateDataGrid()
    {
        Dictionary<IGridCoordinates, MazeCellData> gridData = new Dictionary<IGridCoordinates, MazeCellData>();
        uint id = 0;

        for (int y = 0; y < _size; y++)
        {
            for (int x = y/2; x < _size - y/2; x++)
            {
                CartesianGridCoordinates coordinates = new CartesianGridCoordinates(x, y);
                gridData.Add(coordinates, new SquareMazeCellData(coordinates, id));
                id++;
            }
        }

        return gridData;
    }
}
