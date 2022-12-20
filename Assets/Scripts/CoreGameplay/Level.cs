using System;
using UnityEngine;

public class Level 
{
    private LevelConfig _config;
    private IGameMode _gameMode;

    private Maze _maze;
    private Character _character;

    private MovementHandler _movementHandler;
    private MovementControlMediator _movementControlMediator;

    private MazeSetupBuilderFactory _mazeSetupBuilderFactory;
    private MazeSetupBuilder _mazeSetupBuilder;

    private MazeConfig MazeConfig => _config.MazeConfig;

    public Level(Maze maze, Character character, MovementHandler movementHandler, MovementControlMediator movementControlMediator, LevelConfig config)
    {
        _config = config;

        _maze = maze;
        _character = character;

        _movementHandler = movementHandler;
        _movementControlMediator = movementControlMediator;


        if(config.GameMode == GameModes.Standart)
        {
            _gameMode = new StandartGameMode(_maze, _movementHandler);
            _gameMode.LevelCompleted += OnLevelComplete;
        }

        _maze.Initialize(MazeConfig.CellFactory, MazeConfig.CellContentFactory);

        _mazeSetupBuilderFactory = new MazeSetupBuilderFactory();
    }

    public void StartLevel()
    {
        MazeCellType cellType = MazeConfig.CellType;

        PrepareMaze(cellType);
        PrepareCharacter();
        PrepareControl(cellType);

        _gameMode.StartLevel();
    }

    private void PrepareCharacter()
    {
        _character.Initialize();
        _character.SpawnTo(_maze.StartCell.SurfaceCenterPosition);
    }

    private void PrepareControl(MazeCellType cellType)
    {
        _movementHandler.Disable();

        _movementControlMediator.Initialize(_movementHandler, MovementControlType.WorldSpaceDisplay, cellType, _character.transform, _maze);
        _movementHandler.Initialize(_maze.StartCell);
    }

    private void PrepareMaze(MazeCellType cellType)
    {
        _maze.Clear();

        _mazeSetupBuilder = _mazeSetupBuilderFactory.Get(cellType);
        MazeSetup setup = _mazeSetupBuilder
                               .SetSize(MazeConfig.StartWidth, MazeConfig.StartHeight)
                               .SetGenerationAlgoritm(MazeConfig.GenerationAlgoritm)
                               .SetMazeForm(MazeConfig.FormType)
                               .GetResult();

        _maze.Build(setup);
        _maze.SetRandomStartPosition();
        _maze.SetFinishDistantFromStart();
    }

    private void OnLevelComplete()
    {
        Debug.Log("YOOOOOUUUU WIIIINNNN!!!");
        StartLevel();
    }

    ~Level()
    {
        _gameMode.LevelCompleted -= OnLevelComplete;
    }
}
