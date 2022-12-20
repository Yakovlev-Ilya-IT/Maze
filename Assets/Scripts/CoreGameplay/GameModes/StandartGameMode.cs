using System;

public class StandartGameMode : IGameMode
{
    private MovementHandler _movementControlHandler;
    private Maze _maze;

    public event Action LevelCompleted;

    public StandartGameMode(Maze maze, MovementHandler movementControlHandler)
    {
        _maze = maze;
        _movementControlHandler = movementControlHandler;
    }

    public void StartLevel()
    {
        _movementControlHandler.NewCellHasBeenReached += OnNewCellHasBeenReached;
    }

    private void OnNewCellHasBeenReached(MazeCell cell)
    {
        if (cell == _maze.FinishCell)
        {
            _movementControlHandler.StopMove();
            _movementControlHandler.NewCellHasBeenReached -= OnNewCellHasBeenReached;
            LevelCompleted?.Invoke();
        }
    }
}
