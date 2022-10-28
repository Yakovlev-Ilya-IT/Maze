using UnityEngine;

public abstract class Grid
{
    protected int _width;
    protected int _height;

    protected CellData[,] _cellsData;

    public CellData[,] CellsData => _cellsData;

    public Grid(int width, int height)
    {
        _width = width;
        _height = height;

        Fill();
    }

    private void Fill()
    {
        _cellsData = new CellData[_width, _height];

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

    protected abstract CellData GetNewCell(IGridCoordinates coordinates, uint idCounter);

    public bool TryGetCell(IGridCoordinates coordinates, out CellData cell)
    {
        CartesianGridCoordinates cartesianCoordinates = coordinates.ConvertToGridCartesian();
        int x = cartesianCoordinates.X;
        int y = cartesianCoordinates.Y;

        if (CheckOutOfBounds(x, y))
        {
            cell = _cellsData[x, y];
            return true;
        }

        cell = null;
        return false;
    }

    private bool CheckOutOfBounds(int x, int y) => x >= 0 && x < _width && y >= 0 && y < _height;

    public CellData GetRandomCell() => _cellsData[Random.Range(0, _cellsData.GetLength(0)), Random.Range(0, _cellsData.GetLength(1))];
}
