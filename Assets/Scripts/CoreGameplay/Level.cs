using System;
using System.Collections.Generic;
using UnityEngine;

public class Level 
{
    private LevelConfig _config;

    private Maze _maze;

    private Character _character;

    private MazeSetupBuilderFactory _mazeConfigBuilderFactory;
    private MazeSetupBuilder _mazeConfigBuilder;
    private MazeCellFactory _cellFactory;
    private MazeCellContentFactory _cellContentFactory;

    private MazeMovementHandlerFactory _movementHandlerFactory;
    private IMazeMovementHandler _movementHandler;

    private MazeMovementControlFactory _ñontrolFactory;
    private IMazeMovementControl _control;
    private const string ControlFactoryPath = "Controls";
    private readonly Dictionary<MazeCellType, string> _cellTypeToControlFactoryName = new Dictionary<MazeCellType, string>()
    {
        { MazeCellType.Square, "SquareMovementControlFactory"},
        { MazeCellType.Hex, "HexMovementControlFactory" }
    };

    private MazeConfig MazeConfig => _config.MazeConfig;

    public Level(Maze maze, Character character, LevelConfig config)
    {
        _config = config;
        _maze = maze;
        _character = character;

        _maze.Initialize(MazeConfig.CellFactory, MazeConfig.CellContentFactory);

        _mazeConfigBuilderFactory = new MazeSetupBuilderFactory();
        
        _movementHandlerFactory = new MazeMovementHandlerFactory();
    }

    public void StartLevel()
    {
        MazeCellType cellType = (MazeCellType)UnityEngine.Random.Range(0, Enum.GetNames(typeof(MazeCellType)).Length);

        PrepareMaze(cellType);
        PrepareCharacter();
        PrepareControl(cellType);
    }

    private void PrepareCharacter()
    {
        _character.Initialize();
        _character.SpawnTo(_maze.StartCell.SurfaceCenterPosition);
    }

    private void PrepareControl(MazeCellType cellType)
    {
        _control?.Disable();

        if (_movementHandler != null)
        {
            _movementHandler.TargetReached -= OnTargetReached;
            _movementHandler?.Disable();
        }

        _ñontrolFactory = Resources.Load<MazeMovementControlFactory>($"{ControlFactoryPath}/{_cellTypeToControlFactoryName[cellType]}");

        _control = _ñontrolFactory.Get(MovementControlType.WorldSpaceDisplay, _character.gameObject.transform);
        _movementHandler = _movementHandlerFactory.Get(MovementHandlerType.Automatic, _character, _control, _maze, _maze.StartCell);

        _movementHandler.TargetReached += OnTargetReached;
    }

    private void PrepareMaze(MazeCellType cellType)
    {
        _maze.Clear();

        _mazeConfigBuilder = _mazeConfigBuilderFactory.Get(cellType);
        MazeSetup setup = _mazeConfigBuilder
                               .SetSize(MazeConfig.StartMazeWidth, MazeConfig.StartMazeHeight)
                               .SetGenerationAlgoritm(MazeConfig.MazeGenerationAlgoritm)
                               .SetMazeForm(MazeConfig.MazeFormType)
                               .GetResult();

        _maze.Build(setup);
        _maze.SetRandomStartPosition();
        _maze.SetFinishDistantFromStart();
    }

    private void OnTargetReached(MazeCell cell)
    {
        if (cell == _maze.FinishCell)
        {
            Debug.Log("YOOOOOUUUU WIIIINNNN!!!");
            StartLevel();
        }
    }
}
