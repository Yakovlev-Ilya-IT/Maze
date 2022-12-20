using UnityEngine;

public abstract class MazeMovementControlFactory : ScriptableObject
{
    public abstract IMazeMovementControl Get(MovementControlType type, Transform bindingTarget, IRotatable rotationTarget);
}
