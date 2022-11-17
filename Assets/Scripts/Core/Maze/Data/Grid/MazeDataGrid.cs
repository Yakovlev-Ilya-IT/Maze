using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class MazeDataGrid
{
    private int _width;
    private int _height;

    private Dictionary<IGridCoordinates, MazeCellData> _cellsData = new Dictionary<IGridCoordinates,MazeCellData>();

    public int Width => _width;
    public int Height => _height;
    public Dictionary<IGridCoordinates, MazeCellData> CellsData => _cellsData;

    public MazeDataGrid(int width, int height)
    {
        _width = width;
        _height = height;

        Fill();
    }

    private void Fill()
    {
        _cellsData = new Dictionary<IGridCoordinates, MazeCellData>();

        uint idCounter = 0;

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                MazeCellData cell = GetNewCell(new CartesianGridCoordinates(x, y), idCounter);
                _cellsData.Add(cell.Coordinates, cell);
                idCounter++;
            }
        }
    }

    protected abstract MazeCellData GetNewCell(IGridCoordinates coordinates, uint idCounter);

    public bool TryGetCell(IGridCoordinates coordinates, out MazeCellData cell)
    {
        if (CheckOutOfBounds(coordinates))
        {
            cell = _cellsData[coordinates];
            if (cell != null)
                return true;
            else
                return false;
        }

        cell = null;
        return false;
    }

    private bool CheckOutOfBounds(IGridCoordinates coordinates) => _cellsData.ContainsKey(coordinates);

    public MazeCellData GetRandomCell()
    {
        List<IGridCoordinates> keys = Enumerable.ToList(_cellsData.Keys);

        return _cellsData[keys[Random.Range(0, keys.Count)]];
    }
}
