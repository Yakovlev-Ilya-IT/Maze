using System.Collections.Generic;

public interface IMazeGridForm  
{
    public float XOffset { get; }
    public float YOffset { get; }
    public List<IGridCoordinates> GenerateGridCoordinates();
}
