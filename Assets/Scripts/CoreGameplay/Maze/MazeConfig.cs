using System;
using UnityEngine;

[Serializable]
public class MazeConfig
{
    [SerializeField] private MazeCellFactory _cellFactory;
    [SerializeField] private MazeCellContentFactory _cellContentFactory;

    [SerializeField] private int _startMazeWidth;
    [SerializeField] private int _startMazeHeight;

    [SerializeField] private MazeFormType _mazeFormType;
    [SerializeField] private MazeGenerationAlgorithm _mazeGenerationAlgoritm;

    [SerializeField] private bool _randomMazeFormType;
    [SerializeField] private bool _randomMazeGenerationAlgoritm;

    public MazeCellFactory CellFactory => _cellFactory;
    public MazeCellContentFactory CellContentFactory => _cellContentFactory;

    public int StartMazeWidth => _startMazeWidth;
    public int StartMazeHeight => _startMazeHeight;

    public MazeFormType MazeFormType
    {
        get
        {
            if (_randomMazeFormType)
                return (MazeFormType)UnityEngine.Random.Range(0, Enum.GetNames(typeof(MazeFormType)).Length);

            return _mazeFormType;
        }
    }

    public MazeGenerationAlgorithm MazeGenerationAlgoritm
    {
        get
        {
            if (_randomMazeGenerationAlgoritm)
                return (MazeGenerationAlgorithm)UnityEngine.Random.Range(0, Enum.GetNames(typeof(MazeGenerationAlgorithm)).Length);

            return _mazeGenerationAlgoritm;
        }
    }
}
