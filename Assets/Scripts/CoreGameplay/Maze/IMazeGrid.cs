public interface IMazeGrid 
{ 
    public MazeCell GetRandomCell();
    public bool TryGetCell(IGridCoordinates coordinates, out MazeCell cell);
}
