using System;
using UnityEngine;

public class Maze: MonoBehaviour
{
    private MazeCell[,] _grid;

    private MazeCellType _cellType;
    private int _width;
    private int _height;

    private MazeCell _startCell;
    private MazeCell _finishCell;

    private MazeGridGeneratorFactory _gridGeneratorFactory;
    private MazeCellFactory _cellFactory;
    private MazeCellContentFactory _cellContentFactory;

    public MazeCell[,] Grid => _grid;
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

        _grid = new MazeCell[width, height];

        for (int i = 0; i < dataGrid.Width; i++)
        {
            for (int j = 0; j < dataGrid.Height; j++)
            {
                MazeCellData cellData = dataGrid.CellsData[i, j];

                if (cellData == null)
                    continue;


                MazeCell cell = BuildCell(cellData);

                _grid[i, j] = cell;
            }
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
        while (randomCell == null && randomCell == _finishCell);

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
        while (randomCell == null && randomCell == _startCell);

        randomCell.Content = _cellContentFactory.Get(MazeCellContentType.Finish);

        _finishCell = randomCell;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 10);
    }

    private MazeCell GetRandomCell() => _grid[UnityEngine.Random.Range(0, _width), UnityEngine.Random.Range(0, _height)];

    public void Clear()
    {
        if( _grid != null )
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    Destroy(_grid[i, j].gameObject);
                }
            }
    }
}
