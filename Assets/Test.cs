using System;
using UnityEngine;

public class Test : MonoBehaviour
{
    public MazeCellFactory _cellFactory;
    public SquareMazeMovementControlFactory _squareControlFactory;
    public HexMazeMovementControlFactory _hexControlFactory;
    public MazeCellContentFactory _cellContentFactory;


    public Maze _maze;
    public Character _character;

    public MazeFormType FormType;
    public MazeGenerationAlgorithm GenerationAlgoritm;
    public int Width = 15;
    public int Height = 15;


    IMazeMovementControl control;
    IMazeMovementHandler movementHandler;



    public void StartSquareGame()
    {
        SquareMazeMovementHandlerFactory movementHandlerFactory = new SquareMazeMovementHandlerFactory();

        _maze.Clear();
        control?.Disable();

        if(movementHandler!=null)
            movementHandler.TargetReached -= OnTargetReached;
        movementHandler?.Disable();

        SquareMazeGridGeneratorFactory gridGeneratorFactory = new SquareMazeGridGeneratorFactory();
        SquareMazeFormFactory squareFormFactory = new SquareMazeFormFactory();
        _maze.Initialize(MazeCellType.Square, gridGeneratorFactory, squareFormFactory, _cellFactory, _cellContentFactory);

        _maze.Build(GenerationAlgoritm, FormType, Width, Height);
        _maze.SetRandomStartPosition();
        _maze.SetRandomFinishPosition();

        _character.Initialize();
        _character.SpawnTo(_maze.StartCell.SurfaceCenterPosition);

        control = _squareControlFactory.Get(MovementControlType.WorldSpaceDisplay, _character.gameObject.transform);
        movementHandler = movementHandlerFactory.Get(MovementHandlerType.Automatic, _character, control, _maze, _maze.StartCell);

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
        HexMazeMovementHandlerFactory movementHandlerFactory = new HexMazeMovementHandlerFactory();

        _maze.Clear();
        control?.Disable();

        if (movementHandler != null)
            movementHandler.TargetReached -= OnTargetReached;
        movementHandler?.Disable();

        HexMazeGridGeneratorFactory gridGeneratorFactory = new HexMazeGridGeneratorFactory();
        HexMazeFormFactory squareFormFactory = new HexMazeFormFactory();
        _maze.Initialize(MazeCellType.Hex, gridGeneratorFactory, squareFormFactory, _cellFactory, _cellContentFactory);

        _maze.Build(GenerationAlgoritm, FormType, Width, Height);
        _maze.SetRandomStartPosition();
        _maze.SetRandomFinishPosition();

        _character.Initialize();
        _character.SpawnTo(_maze.StartCell.SurfaceCenterPosition);

        control = _hexControlFactory.Get(MovementControlType.WorldSpaceDisplay, _character.gameObject.transform);
        movementHandler = movementHandlerFactory.Get(MovementHandlerType.Automatic, _character, control, _maze, _maze.StartCell);

        movementHandler.TargetReached += OnTargetReached;
    }

    public void StartNewGame()
    {
        MazeCellType mazeCellType = (MazeCellType)UnityEngine.Random.Range(0, Enum.GetNames(typeof(MazeCellType)).Length);
        MazeFormType mazeFormType = (MazeFormType)UnityEngine.Random.Range(0, Enum.GetNames(typeof(MazeFormType)).Length);

        int width = UnityEngine.Random.Range(6, 15);
        int height = UnityEngine.Random.Range(6, 15);
        MazeGridGeneratorFactory mazeGridGeneratorFactory;
        MazeFormFactory mazeFormFactory;
        MazeMovementHandlerFactory mazeMovementHandlerFactory;
        MazeMovementControlFactory mazeMovementControlFactory;

        if (mazeCellType == MazeCellType.Hex)
        {
            mazeGridGeneratorFactory = new HexMazeGridGeneratorFactory();
            mazeFormFactory = new HexMazeFormFactory();
            mazeMovementHandlerFactory = new HexMazeMovementHandlerFactory();
            mazeMovementControlFactory = _hexControlFactory;
        }
        else
        {
            mazeGridGeneratorFactory = new SquareMazeGridGeneratorFactory();
            mazeFormFactory = new SquareMazeFormFactory();
            mazeMovementHandlerFactory= new SquareMazeMovementHandlerFactory();
            mazeMovementControlFactory = _squareControlFactory;
        }


        _maze.Clear();
        control?.Disable();
        if (movementHandler != null)
            movementHandler.TargetReached -= OnTargetReached;
        movementHandler?.Disable();

        _maze.Initialize(mazeCellType, mazeGridGeneratorFactory, mazeFormFactory, _cellFactory, _cellContentFactory);

        _maze.Build(GenerationAlgoritm, mazeFormType, width, height);
        _maze.SetRandomStartPosition();
        _maze.SetRandomFinishPosition();

        _character.Initialize();
        _character.SpawnTo(_maze.StartCell.SurfaceCenterPosition);

        control = mazeMovementControlFactory.Get(MovementControlType.WorldSpaceDisplay, _character.gameObject.transform);
        movementHandler = mazeMovementHandlerFactory.Get(MovementHandlerType.Automatic, _character, control, _maze, _maze.StartCell);

        movementHandler.TargetReached += OnTargetReached;
    }
}
