using System.Collections.Generic;
using UnityEngine;

public class RectangleSquareMazeForm : IMazeGridForm
{
    private int _width;
    private int _height;

    public float XOffset => _width / 2f - 0.5f;
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

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                gridCoordinates.Add(new CartesianGridCoordinates(x, y));
            }
        }

        return gridCoordinates;
    }
}
