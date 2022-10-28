public class AxialGridCoordinates : IGridCoordinates
{
    private int _q;
    private int _r;

    public int X => _q;
    public int Y => _r;
    public int S => -_q - _r;

    public AxialGridCoordinates(int q, int r)
    {
        _q = q;
        _r = r;
    }

    public AxialGridCoordinates ConvertToGridAxial()
    {
        return this;
    }

    public CartesianGridCoordinates ConvertToGridCartesian()
    {
        int x = _q + (_r + (_r & 1)) / 2;
        int y = _r;

        return new CartesianGridCoordinates(x, y);
    }
}
