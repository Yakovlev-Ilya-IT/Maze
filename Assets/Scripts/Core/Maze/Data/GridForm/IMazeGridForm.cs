public interface IMazeGridForm
{
    public int Width { get; }
    public int Height { get; }

    public bool CheckOutBoundsOfSquareFigure(CartesianGridCoordinates gridCoordinates);
    public bool CheckOutBoundsOfHexFigure(AxialGridCoordinates gridCoordinates);
}
