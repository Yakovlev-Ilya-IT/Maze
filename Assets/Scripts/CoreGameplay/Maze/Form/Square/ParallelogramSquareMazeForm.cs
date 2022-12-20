using System.Collections.Generic;
using UnityEngine;

public class ParallelogramSquareMazeForm : IMazeGridForm
{
    private int _width;
    private int _height;

    public float XOffset => (_width + (_height - 1) / 2f) / 2f - 0.5f;
    public float YOffset => _height / 2f - 0.5f;

    public Quaternion Rotation => Quaternion.Euler(new Vector3(0, 60, 0));

    private void Initialize(int width, int height)
    {
        if (width > height)
        {
            _width = width;
            _height = height;
        }
        else
        {
            _width = height;
            _height = width;
        }
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
