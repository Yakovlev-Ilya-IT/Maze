using System;
using System.Collections.Generic;

public abstract class AutomaticMazeMovementHandler: IMazeMovementHandler
{
    private MazeCell _currentCell;
    private MazeCell _targetCell;

    private Queue<MazeCell> _currentPath;

    private bool _isEnable;

    private IMovable _movable;
    private IMazeMovementControl _movementControl;
    private IMazeGrid _mazeGrid;

    private const int NumberOpenNeighbourCellForMovement = 2;

    public event Action<MazeCell> TargetReached;

    public AutomaticMazeMovementHandler(IMovable movable, IMazeMovementControl movementControl, IMazeGrid mazeGrid, MazeCell currentCell)
    {
        _mazeGrid = mazeGrid;

        SetMovable(movable);
        SetControl(movementControl);

        _isEnable = true;

        _currentCell = currentCell; 
        _movementControl.Show(_currentCell.Walls);
    }

    public void SetControl(IMazeMovementControl movementControl)
    {
        if(_movementControl != null)
            _movementControl.DirectionSelected -= OnDirectionSelected;

        _movementControl = movementControl;
        _movementControl.DirectionSelected += OnDirectionSelected;
    }

    public void SetMovable(IMovable movable)
    {
        if(_movable != null)
            _movable.TargetReached -= OnTargetReached;

        _movable = movable;
        _movable.TargetReached += OnTargetReached;
    }

    private void OnTargetReached()
    {
        _currentCell = _targetCell;
        TargetReached.Invoke(_currentCell);

        if (_currentPath.Count != 0 && _isEnable)
        {
            _targetCell = _currentPath.Dequeue();
            _movable.Move(_currentCell.SurfaceCenterPosition, _targetCell.SurfaceCenterPosition);
        }
        else
        {
            _movementControl.Show(_currentCell.Walls);
            _movable.Stop();
        }
    }

    protected void OnDirectionSelected(CellDirections direction)
    {
        _movementControl.Hide();
        CalculatePath(direction);
        StartMove();
    }

    private void CalculatePath(CellDirections direction)
    {
        _currentPath = new Queue<MazeCell>();

        MazeCell previousCell = _currentCell;

        MazeCell currentCell = GetNextCell(previousCell, direction, _mazeGrid);
        _currentPath.Enqueue(currentCell);

        while (currentCell.OpenNeighbours == NumberOpenNeighbourCellForMovement)
        {
            foreach (KeyValuePair<CellDirections, bool> wall in currentCell.Walls)
            {
                if (wall.Value)
                    continue;

                MazeCell nextCell = GetNextCell(currentCell, wall.Key, _mazeGrid);

                if (nextCell == previousCell)
                    continue;

                _currentPath.Enqueue(nextCell);
                previousCell = currentCell;
                currentCell = nextCell;
                break;
            }
        }
    }

    protected abstract MazeCell GetNextCell(MazeCell previousCell, CellDirections nextCellDirection, IMazeGrid mazeGrid);

    public void StartMove()
    {
        _targetCell = _currentPath.Dequeue();
        _movable.Move(_currentCell.SurfaceCenterPosition, _targetCell.SurfaceCenterPosition);
    }

    public void Disable()
    {
        _isEnable = false;
        _movable.TargetReached -= OnTargetReached;
        _movementControl.DirectionSelected -= OnDirectionSelected;
    }

    ~AutomaticMazeMovementHandler()
    {
        _movable.TargetReached -= OnTargetReached;
        _movementControl.DirectionSelected -= OnDirectionSelected;
    }
}
