public interface IGridCoordinates 
{
    public int X { get; }
    public int Y { get; }

    public AxialGridCoordinates ConvertToGridAxial();
    public CartesianGridCoordinates ConvertToGridCartesian();
}
