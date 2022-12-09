using System;
using UnityEngine;

[Serializable]
public class LevelConfig 
{
    [SerializeField] private MazeConfig _mazeConfig;
    [SerializeField] private GameModes _gameMode;

    public MazeConfig MazeConfig => _mazeConfig;
    public GameModes GameMode => _gameMode;
}
