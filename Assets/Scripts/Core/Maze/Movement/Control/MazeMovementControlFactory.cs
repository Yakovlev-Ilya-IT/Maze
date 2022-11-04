using UnityEngine;

public abstract class MazeMovementControlFactory : ScriptableObject
{
    public abstract IMazeMovementControl Get(MazeCellType type, Transform bindingTarget);
}
