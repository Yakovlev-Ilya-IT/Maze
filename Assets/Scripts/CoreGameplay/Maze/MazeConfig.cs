using System;
using UnityEngine;

[Serializable]
public class MazeConfig
{
    [SerializeField] private MazeCellFactory _cellFactory;
    [SerializeField] private MazeCellContentFactory _cellContentFactory;

    [SerializeField] private int _startWidth;
    [SerializeField] private int _startHeight;

    [SerializeField] private MazeCellType _cellType;
    [SerializeField] private MazeFormType _formType;
    [SerializeField] private MazeGenerationAlgorithm _generationAlgoritm;

    [SerializeField] private bool _randomCellType;
    [SerializeField] private bool _randomFormType;
    [SerializeField] private bool _randomGenerationAlgoritm;

    public MazeCellFactory CellFactory => _cellFactory;
    public MazeCellContentFactory CellContentFactory => _cellContentFactory;

    public int StartWidth => _startWidth;
    public int StartHeight => _startHeight;

    public MazeCellType CellType
    {
        get
        {
            if (_randomCellType)
                return (MazeCellType)UnityEngine.Random.Range(0, Enum.GetNames(typeof(MazeCellType)).Length);

            return _cellType;
        }
    }

    public MazeFormType FormType
    {
        get
        {
            if (_randomFormType)
                return (MazeFormType)UnityEngine.Random.Range(0, Enum.GetNames(typeof(MazeFormType)).Length);

            return _formType;
        }
    }

    public MazeGenerationAlgorithm GenerationAlgoritm
    {
        get
        {
            if (_randomGenerationAlgoritm)
                return (MazeGenerationAlgorithm)UnityEngine.Random.Range(0, Enum.GetNames(typeof(MazeGenerationAlgorithm)).Length);

            return _generationAlgoritm;
        }
    }
}
