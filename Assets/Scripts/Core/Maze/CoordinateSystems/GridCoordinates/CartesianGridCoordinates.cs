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

    public CartesianGridCoordinates ConvertToGridCartesian() => this;

    public void ConvertToScaledCartesian(float scaleMultiplier, out float x, out float y)
    {
        x = _x * scaleMultiplier;
        y = _y * scaleMultiplier;
    }

    public static CartesianGridCoordinates operator +(CartesianGridCoordinates a, CartesianGridCoordinates b) => new CartesianGridCoordinates(a._x + b._x, a._y + b._y);
}
