using System.Collections.Generic;
using UnityEngine;

public class RectangleHexMazeForm : IMazeGridForm
{
    private int _width;
    private int _height;

    public float XOffset => _width / 2f;
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

        for (int r = 0; r < _height; r++)
        {
            int rOffset = Mathf.CeilToInt(r / 2f);
            for (int q = 0 - rOffset; q < _width - rOffset; q++)
            {
                gridCoordinates.Add(new AxialGridCoordinates(q, r));
            }
        }

        return gridCoordinates;
    }
}
