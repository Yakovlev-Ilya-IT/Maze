using System;

public interface IMazeMovementHandler
{
    public event Action<MazeCell> TargetReached;

    public void SetControl(IMazeMovementControl movementControl);
    public void SetMovable(IMovable movable);
    public void Disable();
}
