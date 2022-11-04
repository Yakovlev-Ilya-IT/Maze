using UnityEngine;

public abstract class MazeDataGrid
{
    private int _width;
    private int _height;

    private MazeCellData[,] _cellsData;

    public int Width => _width;
    public int Height => _height;
    public MazeCellData[,] CellsData => _cellsData;

    public MazeDataGrid(int width, int height)
    {
        _width = width;
        _height = height;

        Fill();
    }

    private void Fill()
    {
        _cellsData = new MazeCellData[_width, _height];

        uint idCounter = 0;

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                _cellsData[x, y] = GetNewCell(new CartesianGridCoordinates(x, y), idCounter);
                idCounter++;
            }
        }
    }

    protected abstract MazeCellData GetNewCell(IGridCoordinates coordinates, uint idCounter);

    public bool TryGetCell(IGridCoordinates coordinates, out MazeCellData cell)
    {
        CartesianGridCoordinates cartesianCoordinates = coordinates.ConvertToGridCartesian();
        int x = cartesianCoordinates.X;
        int y = cartesianCoordinates.Y;

        if (CheckOutOfBounds(x, y))
        {
            cell = _cellsData[x, y];
            if (cell != null)
                return true;
            else
                return false;
        }

        cell = null;
        return false;
    }

    private bool CheckOutOfBounds(int x, int y) => x >= 0 && x < _width && y >= 0 && y < _height;

    public MazeCellData GetRandomCell()
    {
        MazeCellData randomCell;

        do
        {
            randomCell = _cellsData[Random.Range(0, _cellsData.GetLength(0)), Random.Range(0, _cellsData.GetLength(1))];
        } while (randomCell == null);

        return randomCell;
    }
}
