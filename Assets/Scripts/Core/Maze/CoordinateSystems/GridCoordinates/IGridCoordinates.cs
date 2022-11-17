using System;

public interface IGridCoordinates: IEquatable<IGridCoordinates>
{
    public int X { get; }
    public int Y { get; }

    public AxialGridCoordinates ConvertToGridAxial();
    public CartesianGridCoordinates ConvertToGridCartesian();
    public void ConvertToScaledCartesian(float scaleMultiplier, out float x, out float y);
}
