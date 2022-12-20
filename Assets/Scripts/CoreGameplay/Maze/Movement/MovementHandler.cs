using System;
using System.Collections.Generic;

public class MovementHandler: IDisposable
{
    private MovementPathfinderFactory _pathfinderFactory;
    private IMovementPathfinder _pathfinder;

    private MazeCell _currentCell;
    private MazeCell _nextCell;
    private Queue<MazeCell> _currentPath;

    public event Action<MazeCell> NewCellHasBeenReached;

    private MovementControlMediator _mediator;
    private IMazeGrid _mazeGrid;
    private IMovable _movable;

    private bool _isInit;
    private bool _isStop;

    public IMovable Movable
    {
        get => _movable;
        set
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            _movable = value;
        }
    }

    public MovementHandler(MovementControlMediator mediator, IMazeGrid mazeGrid, IMovable movable, MovementPathfinderType pathfinderType)
    {
        _mediator = mediator;
        _mazeGrid = mazeGrid;
        Movable = movable;

        _pathfinderFactory = new MovementPathfinderFactory();
        SetPathfinder(pathfinderType);
    }

    public void Initialize(MazeCell currentCell)
    {
        _currentCell = currentCell;

        ShowControl();

        _isInit = true;
    }

    public void Disable()
    {
        Dispose();
        _isInit = false;
        _isStop = true;
    }

    public void StartMove(CellDirections direction)
    {
        if (!_isInit)
            throw new InvalidOperationException("MovementHandler not inited");

        HideControl();

        _currentPath = _pathfinder.CalculatePath(_currentCell, direction);

        _nextCell = _currentPath.Dequeue();

        _movable.TargetReached += OnMovableTargetReached;
        _movable.Move(_currentCell.SurfaceCenterPosition, _nextCell.SurfaceCenterPosition);

        _isStop = false;
    }

    public void StopMove()
    {
        _movable.Stop();
        _movable.TargetReached -= OnMovableTargetReached;

        ShowControl();

        _isStop = true;
    }

    private void OnMovableTargetReached()
    {
        _currentCell = _nextCell;
        NewCellHasBeenReached?.Invoke(_currentCell);

        if (_isStop)
            return;

        if (_currentPath.Count != 0)
        {
            _nextCell = _currentPath.Dequeue();
            _movable.Move(_currentCell.SurfaceCenterPosition, _nextCell.SurfaceCenterPosition);
            return;
        }

        StopMove();
    }

    public void SetPathfinder(MovementPathfinderType pathfinderType) => _pathfinder = _pathfinderFactory.Get(pathfinderType, _mazeGrid);

    private void ShowControl() => _mediator.ShowControl(_currentCell.DirectionToNeighboursCoordinates);
    private void HideControl() => _mediator.HideControl();

    public void Dispose()
    {
        _movable.TargetReached -= OnMovableTargetReached;
    }
}
