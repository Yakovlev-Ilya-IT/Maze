using System.Collections.Generic;
using UnityEngine;

public interface IMazeGridForm  
{
    public float XOffset { get; }
    public float YOffset { get; }
    public Quaternion Rotation { get; }
    public List<IGridCoordinates> GenerateGridCoordinates(int width, int height);
}
