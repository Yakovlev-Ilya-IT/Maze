using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Maze: MonoBehaviour, IMazeGrid
{
    private Dictionary<IGridCoordinates, MazeCell> _grid = new Dictionary<IGridCoordinates, MazeCell>();

    private MazeCellType _cellType;
    private IMazeGridForm _form;

    private MazeCell _startCell;
    private MazeCell _finishCell;

    private MazeGridGeneratorFactory _gridGeneratorFactory;
    private MazeFormFactory _formFactory;
    private MazeCellFactory _cellFactory;
    private MazeCellContentFactory _cellContentFactory;

    public Dictionary<IGridCoordinates, MazeCell> Grid => _grid;
    public MazeCell StartCell => _startCell;
    public MazeCell FinishCell => _finishCell;

    public void Initialize(MazeCellType cellType, MazeGridGeneratorFactory gridGeneratorFactory, MazeFormFactory formFactory, MazeCellFactory cellFactory, MazeCellContentFactory cellContentFactory)
    {
        _cellType = cellType;

        _gridGeneratorFactory = gridGeneratorFactory;
        _formFactory = formFactory;
        _cellFactory = cellFactory;
        _cellContentFactory = cellContentFactory;
    }

    public void Build(MazeGenerationAlgorithm generationAlgoritm, MazeFormType formType, int width, int height)
    {
        if (width <= 1 || height <= 1)
            throw new ArgumentException("Error widtg or height for maze");

        IMazeGridGenerator gridGenerator = _gridGeneratorFactory.Get(generationAlgoritm);
        _form = _formFactory.Get(formType, width, height);

        MazeDataGrid dataGrid = gridGenerator.Generate(_form);

        _grid = new Dictionary<IGridCoordinates, MazeCell>();
        
        foreach (KeyValuePair<IGridCoordinates, MazeCellData> data in dataGrid.CellsData)
        {
            MazeCellData cellData = data.Value;
            if (cellData == null)
                continue;

            MazeCell cell = BuildCell(cellData);
            _grid.Add(data.Key, cell);
        }
    }

    private MazeCell BuildCell(MazeCellData data)
    {
        MazeCellContent cellContent = _cellContentFactory.Get(MazeCellContentType.Empty);

        MazeCell cell = _cellFactory.Get(_cellType, data, cellContent);

        Vector2 cellCartesianCoordinates = cell.GetScaledCartesianCoordinate();

        float OffsetX = cell.SizeX * _form.XOffset;
        float OffsetY = cell.SizeZ * _form.YOffset;

        Vector3 position = new Vector3(cellCartesianCoordinates.x - OffsetX, 0, cellCartesianCoordinates.y - OffsetY);

        cell.SpawnTo(position);

        cell.transform.SetParent(transform);

        return cell;
    }

    public void SetRandomStartPosition()
    {
        MazeCell randomCell;

        do
        {
            randomCell = GetRandomCell();
        }
        while (randomCell == _finishCell);

        randomCell.Content = _cellContentFactory.Get(MazeCellContentType.Start);

        _startCell = randomCell;
    }

    public void SetRandomFinishPosition()
    {
        MazeCell randomCell;

        do
        {
            randomCell = GetRandomCell();
        }
        while (randomCell == _startCell);

        randomCell.Content = _cellContentFactory.Get(MazeCellContentType.Finish);

        _finishCell = randomCell;
    }

    public void SetStartPoint(IGridCoordinates coordinates)
    {
        if (TryGetCell(coordinates, out MazeCell cell))
        {
            if (cell == _finishCell)
                _finishCell = null;

            cell.Content = _cellContentFactory.Get(MazeCellContentType.Start);
            _startCell = cell;
        }
        else
        {
            throw new ArgumentException("error coordinates");
        }
    }

    public void SetFinishPoint(IGridCoordinates coordinates)
    {
        if (TryGetCell(coordinates, out MazeCell cell))
        {
            if (cell == _startCell)
                _startCell = null;

            cell.Content = _cellContentFactory.Get(MazeCellContentType.Finish);
            _finishCell = cell;
        }
        else
        {
            throw new ArgumentException("error coordinates");
        }
    }

    //public void SetFinishDistantFromStart()
    //{
    //    if (_startCell == null)
    //        throw new NullReferenceException("StartCell is null");

    //    List<IGridCoordinates> visitedCellsCoordinates = new List<IGridCoordinates>();

    //    MazeCell currentCell = _startCell;
    //    MazeCell finishCell;

    //    visitedCellsCoordinates.Add(currentCell.GridCoordinates);

    //    Stack<MazeCell> mazeCells = new Stack<MazeCell>();

    //    mazeCells.Push(currentCell);

    //    while (mazeCells.Count > 0)
    //    {
    //        currentCell = mazeCells.Pop();

    //        List<MazeCell> unvisitedNeighbours = GetUnvisitedNeighbours(currentCell, mazeGrid, visitedCells);

    //        if (unvisitedNeighbours.Count > 0)
    //        {
    //            mazeCells.Push(currentCell);

    //            MazeCellData chosenCell = unvisitedNeighbours[Random.Range(0, unvisitedNeighbours.Count)];

    //            visitedCells.Add(chosenCell.Id, chosenCell);
    //            mazeCells.Push(chosenCell);
    //        }
    //    }
    //}

    //private List<MazeCell> GetUnvisitedNeighbours(MazeCell currentCell, List<IGridCoordinates> visitedCellsCoordinates)
    //{
    //    foreach (KeyValuePair<CellDirections, bool> wall in currentCell.Walls)
    //    {

    //    }
    //}

    public MazeCell GetRandomCell()
    {
        List<IGridCoordinates> keys = Enumerable.ToList(_grid.Keys);

        return _grid[keys[UnityEngine.Random.Range(0, keys.Count)]];
    }

    public bool TryGetCell(IGridCoordinates coordinates, out MazeCell cell)
    {
        if (CheckOutOfBounds(coordinates))
        {
            cell = _grid[coordinates];
            if (cell != null)
                return true;
            else
                return false;
        }

        cell = null;
        return false;
    }

    private bool CheckOutOfBounds(IGridCoordinates coordinates) => _grid.ContainsKey(coordinates);

    public void Clear()
    {
        _startCell = null;
        _finishCell = null;

        if (_grid != null)
            foreach (KeyValuePair<IGridCoordinates, MazeCell> cell in _grid)
                Destroy(cell.Value.gameObject);
    }
}
