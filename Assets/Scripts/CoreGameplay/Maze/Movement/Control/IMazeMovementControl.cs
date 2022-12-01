using System;
using System.Collections.Generic;

public interface IMazeMovementControl
{
    public event Action<CellDirections> DirectionSelected;

    public void Show(Dictionary<CellDirections, IGridCoordinates> directionToNeighboursCoordinates);
    public void Hide();
    public void Disable();
}
