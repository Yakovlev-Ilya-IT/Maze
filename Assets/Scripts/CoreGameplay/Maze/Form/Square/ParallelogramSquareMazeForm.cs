using System.Collections.Generic;

public class ParallelogramSquareMazeForm : IMazeGridForm
{
    private int _width;
    private int _height;

    public float XOffset => (_width + (_height - 1) / 2f) / 2f - 0.5f;
    public float YOffset => _height / 2f - 0.5f;

    private void Initialize(int width, int height)
    {
        _width = width;
        _height = height;
    }

    public List<IGridCoordinates> GenerateGridCoordinates(int width, int height)
    {
        Initialize(width, height);

        List<IGridCoordinates> gridCoordinates = new List<IGridCoordinates>();

        for (int y = 0; y < _height; y++)
        {
            for (int x = y/2; x < _width + y/2; x++)
            {
                gridCoordinates.Add(new CartesianGridCoordinates(x, y));
            }
        }

        return gridCoordinates;
    }
}
