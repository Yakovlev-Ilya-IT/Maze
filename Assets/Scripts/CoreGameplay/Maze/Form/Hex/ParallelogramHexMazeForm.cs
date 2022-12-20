using System.Collections.Generic;
using UnityEngine;

public class ParallelogramHexMazeForm : IMazeGridForm
{
    private int _width;
    private int _height;

    public float XOffset => (_width + _height / Mathf.Tan(60 * Mathf.Deg2Rad)) / 2f - 0.5f;
    public float YOffset => _height / 2f - 0.5f;

    public Quaternion Rotation => Quaternion.Euler(new Vector3(0, 60, 0));

    private void Initialize(int width, int height)
    {
        if(width > height)
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

        for (int q = 0; q < _width; q++)
        {
            for (int r = 0; r < _height; r++)
            {
                gridCoordinates.Add(new AxialGridCoordinates(q, r));
            }
        }

        return gridCoordinates;
    }
}
