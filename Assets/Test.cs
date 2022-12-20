using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private Maze _maze;
    [SerializeField] private Character _character;

    Level _level;
    public void StartGame()
    {
        MovementControlMediator movementMediator = new MovementControlMediator();
        MovementHandler movementControlHandler = new MovementHandler(movementMediator, _maze, _character, MovementPathfinderType.UntilNextFork);

        if (_level == null)
        {
            _level = new Level(_maze, _character, movementControlHandler, movementMediator, _levelConfig);
        }

        _level.StartLevel();
    }
}
