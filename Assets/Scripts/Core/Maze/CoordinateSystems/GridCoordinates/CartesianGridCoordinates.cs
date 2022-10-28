public class CartesianGridCoordinates : IGridCoordinates
{
    private int _x;
    private int _y;

    public int X => _x;

    public int Y => _y;

    public CartesianGridCoordinates(int x, int y)
    {
        _x = x;
        _y = y; 
    }

    public AxialGridCoordinates ConvertToGridAxial()
    {
        int q = _x - (_y + (_y & 1)) / 2;
        int r = _y;

        return new AxialGridCoordinates(q, r);
    }

    public CartesianGridCoordinates ConvertToGridCartesian()
    {
        return this;
    }
}
