using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MazeCellContentFactory", menuName = "Factories/CellContent")]
public class MazeCellContentFactory : ScriptableObject
{
    [SerializeField] private MazeCellContent _emptyPrefab;
    [SerializeField] private MazeCellContent _startPrefab;
    [SerializeField] private MazeCellContent _finishPrefab;

    public MazeCellContent Get(MazeCellContentType type)
    {
        MazeCellContent instance = Instantiate(GetContent(type));
        return instance;
    }

    private MazeCellContent GetContent(MazeCellContentType type)
    {
        switch (type)
        {
            case MazeCellContentType.Empty:
                return _emptyPrefab;
            case MazeCellContentType.Start:
                return _startPrefab;
            case MazeCellContentType.Finish:
                return _finishPrefab;
        }

        throw new ArgumentException($"No prefab for {type}");
    }
}