using UnityEngine;

public class AxialGridCoordinates : IGridCoordinates
{
    private int _q;
    private int _r;

    private const float OffsetBetweenCellsByY = 3f / 4f;
    private const float HeightMultiplayer = 2;

    public int X => _q;
    public int Y => _r;
    public int S => -_q - _r;
    private float WidthMultiplyaer => Mathf.Sqrt(3);

    public AxialGridCoordinates(int q, int r)
    {
        _q = q;
        _r = r;
    }

    public AxialGridCoordinates ConvertToGridAxial() => this;

    public CartesianGridCoordinates ConvertToGridCartesian()
    {
        int x = _q + (_r + (_r & 1)) / 2;
        int y = _r;

        return new CartesianGridCoordinates(x, y);
    }

    public void ConvertToScaledCartesian(float scaleMultiplier, out float x, out float y)
    {
        float height = scaleMultiplier * HeightMultiplayer;
        float width = scaleMultiplier * WidthMultiplyaer;
        float offset;

        if (_r % 2 == 0)
            offset = width / 2;
        else
            offset = 0;

        CartesianGridCoordinates cartesianCoordinates = ConvertToGridCartesian();

        x = cartesianCoordinates.X * width + offset;
        y = cartesianCoordinates.Y * height * OffsetBetweenCellsByY;
    }

    public static AxialGridCoordinates operator +(AxialGridCoordinates a, AxialGridCoordinates b) => new AxialGridCoordinates(a._q + b._q, a._r + b._r);
}
