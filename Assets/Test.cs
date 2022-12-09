using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private Maze _maze;
    [SerializeField] private Character _character;

    Level _level;
    public void StartGame()
    {
        if(_level == null)
            _level = new Level(_maze, _character, _levelConfig);

        _level.StartLevel();
    }
}
