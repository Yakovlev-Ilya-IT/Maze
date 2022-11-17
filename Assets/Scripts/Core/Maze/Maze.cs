using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Maze: MonoBehaviour, IMazeGrid
{
    private Dictionary<IGridCoordinates, MazeCell> _grid = new Dictionary<IGridCoordinates, MazeCell>();

    private MazeCellType _cellType;
    private int _width;
    private int _height;

    private MazeCell _startCell;
    private MazeCell _finishCell;

    private MazeGridGeneratorFactory _gridGeneratorFactory;
    private MazeCellFactory _cellFactory;
    private MazeCellContentFactory _cellContentFactory;

    public Dictionary<IGridCoordinates, MazeCell> Grid => _grid;
    public MazeCell StartCell => _startCell;
    public MazeCell FinishCell => _finishCell;

    public MazeGridGeneratorFactory GridGeneratorFactory
    {
        set
        {
            if (value == null)
                throw new NullReferenceException(nameof(_gridGeneratorFactory));
            _gridGeneratorFactory = value;
        }
    }

    public MazeCellFactory CellFactory
    {
        set
        {
            if (value == null)
                throw new NullReferenceException(nameof(_cellFactory));
            _cellFactory = value;
        }
    }

    public MazeCellContentFactory CellContentFactory
    {
        set
        {
            if(value == null)
                throw new NullReferenceException(nameof(_cellContentFactory));
            _cellContentFactory = value;
        }
    }

    public void Initialize(MazeGridGeneratorFactory gridGeneratorFactory, MazeCellFactory cellFactory, MazeCellContentFactory cellContentFactory)
    {
        GridGeneratorFactory = gridGeneratorFactory;
        CellFactory = cellFactory;
        CellContentFactory = cellContentFactory;
    }

    public void Build(MazeCellType type, int width, int height)
    {
        _cellType = type;
        _width = width;
        _height = height;

        IMazeGridGenerator gridGenerator = _gridGeneratorFactory.Get(type);

        MazeDataGrid dataGrid = gridGenerator.Generate(width, height);

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

        float OffsetX = cell.SizeX * (_width - cell.SizeX * 0.5f) * 0.5f;
        float OffsetY = cell.SizeZ * (_height - cell.SizeZ * 0.5f) * 0.5f;

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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 10);
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
        if (_grid != null)
            foreach (KeyValuePair<IGridCoordinates, MazeCell> cell in _grid)
                Destroy(cell.Value.gameObject);
    }
}
