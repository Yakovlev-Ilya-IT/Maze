using System.Collections.Generic;

public interface IMovementPathfinder 
{
    public Queue<MazeCell> CalculatePath(MazeCell startCell, CellDirections direction);
}
