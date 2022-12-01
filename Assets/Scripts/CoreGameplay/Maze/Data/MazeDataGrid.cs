using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeDataGrid
{
    private IMazeGridForm _form;

    private Dictionary<IGridCoordinates, MazeCellData> _cellsData;

    public Dictionary<IGridCoordinates, MazeCellData> CellsData => _cellsData;

    public MazeDataGrid(IMazeGridForm form)
    {
        _form = form;

        FillGrid();
    }

    private void FillGrid()
    {
        List<IGridCoordinates> gridCoordinates = _form.GenerateGridCoordinates();
        _cellsData = new Dictionary<IGridCoordinates, MazeCellData>();

        uint id = 0;
        foreach (IGridCoordinates coordinates in gridCoordinates)
        {
            _cellsData.Add(coordinates, new MazeCellData(coordinates, id));
            id++;
        }
    }

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
