using UnityEngine;

[CreateAssetMenu(fileName = "MazeCellFactory", menuName = "Factories/MazeCell")]
public class MazeCellFactory: ScriptableObject
{
    [SerializeField] private MazeCell _squareCellPrefab;
    [SerializeField] private MazeCell _hexCellPrefab;

    public MazeCell Get(MazeCellType type, MazeCellData data, MazeCellContent content)
    {
        MazeCell instance = Instantiate(GetMazeCell(type));
        instance.Initialize(data, content);
        return instance;
    }

    private MazeCell GetMazeCell(MazeCellType type)
    {
        switch (type)
        {
            case MazeCellType.Square:
                return _squareCellPrefab;
            case MazeCellType.Hex:
                return _hexCellPrefab;
        }

        Debug.LogError($"No prefab for {type}");
        return _squareCellPrefab;
    }
}
