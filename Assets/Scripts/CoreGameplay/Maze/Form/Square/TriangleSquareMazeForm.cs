using System.Collections.Generic;
using UnityEngine;

public class TriangleSquareMazeForm : IMazeGridForm
{
    private int _size;

    public float XOffset => (_size + _size / 2f) / 3f - 0.5f;
    public float YOffset => _size / 3f;

    public Quaternion Rotation => Quaternion.Euler(new Vector3(0, 100, 0));

    private void Initialize(int width, int height)
    {
        if (width > height)
            _size = width;
        else
            _size = height;
    }

    public List<IGridCoordinates> GenerateGridCoordinates(int width, int height)
    {
        Initialize(width, height);

        List<IGridCoordinates> gridCoordinates = new List<IGridCoordinates>();

        for (int y = 0; y < _size; y++)
        {
            for (int x = y/2; x < _size - y/2; x++)
            {
                gridCoordinates.Add(new CartesianGridCoordinates(x, y));
            }
        }

        return gridCoordinates;
    }
}
