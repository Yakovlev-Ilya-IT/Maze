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

    public Dictionary<IGridCoordinates, MazeCellData> GenerateDataGrid()
    {
        Dictionary<IGridCoordinates, MazeCellData> gridData = new Dictionary<IGridCoordinates, MazeCellData>();
        uint id = 0;

        for (int q = 0; q < _size; q++)
        {
            for (int r = 0; r < _size - q; r++)
            {
                AxialGridCoordinates coordinates = new AxialGridCoordinates(q, r);
                gridData.Add(coordinates, new HexMazeCellData(coordinates, id));
                id++;
            }
        }

        return gridData;
    }
}
