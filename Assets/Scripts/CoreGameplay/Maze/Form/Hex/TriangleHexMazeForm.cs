using System.Collections.Generic;

public class TriangleHexMazeForm : IMazeGridForm
{
    private int _size;

    public float XOffset => (_size + _size / 2f) / 3f;
    public float YOffset => _size / 3f;

    public TriangleHexMazeForm(int width, int height)
    {
        if(width > height)
            _size = width;
        else
            _size = height;
    }

    public List<IGridCoordinates> GenerateGridCoordinates()
    {
        List<IGridCoordinates> gridCoordinates = new List<IGridCoordinates>();

        for (int q = 0; q < _size; q++)
        {
            for (int r = 0; r < _size - q; r++)
            {
                gridCoordinates.Add(new AxialGridCoordinates(q, r));
            }
        }

        return gridCoordinates;
    }
}