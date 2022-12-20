using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Maze: MonoBehaviour, IMazeGrid, IRotatable
{
    private Dictionary<IGridCoordinates, MazeCell> _grid = new Dictionary<IGridCoordinates, MazeCell>();

    private MazeSetup _setup;

    private MazeCell _startCell;
    private MazeCell _finishCell;

    private MazeCellFactory _cellFactory;
    private MazeCellContentFactory _cellContentFactory;

    public Dictionary<IGridCoordinates, MazeCell> Grid => _grid;
    public MazeCell StartCell => _startCell;
    public MazeCell FinishCell => _finishCell;

    private Quaternion InitializeRotation => new Quaternion(0,0,0,0);
    private int Width => _setup.Width;
    private int Height => _setup.Height;
    public MazeCellType CellType => _setup.CellType;
    public IMazeGridForm Form => _setup.Form;
    public IMazeGridGenerator GridGenerator => _setup.GridGenerator;

    public Quaternion Rotation => transform.rotation;

    public void Initialize(MazeCellFactory cellFactory, MazeCellContentFactory cellContentFactory)
    {
        _cellFactory = cellFactory;
        _cellContentFactory = cellContentFactory;
    }

    public void Build(MazeSetup setup)
    {
        transform.rotation = InitializeRotation;

        _setup = setup;

        MazeDataGrid dataGrid = GridGenerator.Generate(Form, Width, Height);

        _grid = new Dictionary<IGridCoordinates, MazeCell>();
        
        foreach (KeyValuePair<IGridCoordinates, MazeCellData> data in dataGrid.CellsData)
        {
            MazeCellData cellData = data.Value;
            if (cellData == null)
                continue;

            MazeCell cell = BuildCell(cellData);
            _grid.Add(data.Key, cell);
        }

        transform.rotation = Form.Rotation;
    }

    private MazeCell BuildCell(MazeCellData data)
    {
        MazeCellContent cellContent = _cellContentFactory.Get(MazeCellContentType.Empty);

        MazeCell cell = _cellFactory.Get(CellType, data, cellContent);

        Vector2 cellCartesianCoordinates = cell.GetScaledCartesianCoordinate();

        float OffsetX = cell.SizeX * Form.XOffset;
        float OffsetY = cell.SizeZ * Form.YOffset;

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

    public void SetFinishDistantFromStart()
    {
        if (_startCell == null)
            throw new NullReferenceException("StartCell is null");

        List<IGridCoordinates> visitedCellsCoordinates = new List<IGridCoordinates>();

        MazeCell currentCell = _startCell;
        MazeCell finishCell = null;
        int distanceFromStart = 0;

        visitedCellsCoordinates.Add(currentCell.GridCoordinates);

        Stack<MazeCell> mazeCells = new Stack<MazeCell>();

        mazeCells.Push(currentCell);

        while (mazeCells.Count > 0)
        {
            currentCell = mazeCells.Pop();

            List<MazeCell> unvisitedNeighbours = GetUnvisitedNeighbours(currentCell, visitedCellsCoordinates);

            if (unvisitedNeighbours.Count > 0)
            {
                mazeCells.Push(currentCell);

                MazeCell chosenCell = unvisitedNeighbours[UnityEngine.Random.Range(0, unvisitedNeighbours.Count)];

                visitedCellsCoordinates.Add(chosenCell.GridCoordinates);
                mazeCells.Push(chosenCell);

                if (distanceFromStart < mazeCells.Count)
                {
                    finishCell = chosenCell;
                    distanceFromStart = mazeCells.Count;
                }
            }
        }

        if(finishCell == null)
            throw new Exception("finish cell is null");

        finishCell.Content = _cellContentFactory.Get(MazeCellContentType.Finish);
        _finishCell = finishCell;
    }

    private List<MazeCell> GetUnvisitedNeighbours(MazeCell currentCell, List<IGridCoordinates> visitedCellsCoordinates)
    {
        List<MazeCell> unvisitedNeighbours = new List<MazeCell>();

        foreach (KeyValuePair<CellDirections, IGridCoordinates> neighbour in currentCell.DirectionToNeighboursCoordinates)
        {
            if (visitedCellsCoordinates.Contains(neighbour.Value))
                continue;

            if (TryGetCell(neighbour.Value, out MazeCell cell))
                unvisitedNeighbours.Add(cell);
            else
                throw new Exception("error coordinates");
        }

        return unvisitedNeighbours;
    }

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
