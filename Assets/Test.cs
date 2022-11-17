using System;
using UnityEngine;

public class Test : MonoBehaviour
{
    public MazeCellFactory _cellFactory;
    public MazeMovementControlFactory _controlFactory;
    public MazeCellContentFactory _cellContentFactory;
    public Maze _maze;
    public MazeCellType _type;
    public Character _character;

    public int Width = 15;
    public int Height = 15;

    public CellDirections direction;


    MazeGridGeneratorFactory gridGeneratorFactory;
    AutomaticMazeMovementHandlerFactory automaticMazeMovementHandlerFactory;

    IMazeMovementControl control;
    IMazeMovementHandler movementHandler;

    private void Start()
    {
        gridGeneratorFactory = new MazeGridGeneratorFactory();
        automaticMazeMovementHandlerFactory = new AutomaticMazeMovementHandlerFactory();

        _maze.Initialize(gridGeneratorFactory, _cellFactory, _cellContentFactory);
    }

    public void StartSquareGame()
    {
        _maze.Clear();
        control?.Disable();
        if(movementHandler!=null)
            movementHandler.TargetReached -= OnTargetReached;
        movementHandler?.Disable();


        _maze.Build(MazeCellType.Square, Width, Height);
        _maze.SetRandomStartPosition();
        _maze.SetRandomFinishPosition();

        _character.Initialize();
        _character.SpawnTo(_maze.StartCell.SurfaceCenterPosition);

        control = _controlFactory.Get(MazeCellType.Square, _character.gameObject.transform);
        movementHandler = automaticMazeMovementHandlerFactory.Get(MazeCellType.Square, _character, control, _maze, _maze.StartCell);

        movementHandler.TargetReached += OnTargetReached;
    }

    private void OnTargetReached(MazeCell cell)
    {
        if (cell == _maze.FinishCell)
        {
            Debug.Log("YOOOOOUUUU WIIIINNNN!!!");
            StartNewGame();
        }
    }

    public void StartHexGame()
    {
        _maze.Clear();
        control?.Disable();
        if (movementHandler != null)
            movementHandler.TargetReached -= OnTargetReached;
        movementHandler?.Disable();

        _maze.Build(MazeCellType.Hex, Width, Height);
        _maze.SetRandomStartPosition();
        _maze.SetRandomFinishPosition();

        _character.Initialize();
        _character.SpawnTo(_maze.StartCell.SurfaceCenterPosition);

        control = _controlFactory.Get(MazeCellType.Hex, _character.gameObject.transform);
        movementHandler = automaticMazeMovementHandlerFactory.Get(MazeCellType.Hex, _character, control, _maze, _maze.StartCell);
        movementHandler.TargetReached += OnTargetReached;
    }

    public void StartNewGame()
    {
        MazeCellType mazeCellType = (MazeCellType)UnityEngine.Random.Range(0, Enum.GetNames(typeof(MazeCellType)).Length);

        _maze.Clear();
        control?.Disable();
        if (movementHandler != null)
            movementHandler.TargetReached -= OnTargetReached;
        movementHandler?.Disable();

        _maze.Build(mazeCellType, Width, Height);
        _maze.SetRandomStartPosition();
        _maze.SetRandomFinishPosition();

        _character.Initialize();
        _character.SpawnTo(_maze.StartCell.SurfaceCenterPosition);

        control = _controlFactory.Get(mazeCellType, _character.gameObject.transform);
        movementHandler = automaticMazeMovementHandlerFactory.Get(mazeCellType, _character, control, _maze, _maze.StartCell);
        movementHandler.TargetReached += OnTargetReached;
    }
}
