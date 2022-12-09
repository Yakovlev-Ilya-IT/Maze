using System;

public class MazeSetup
{
    private int _width;
    private int _height;

    private const int MinWidth = 2;
    private const int MinHeight = 2;

    public int Width
    {
        get => _width;
        set
        {
            if (value < MinWidth)
                throw new ArgumentOutOfRangeException("width");

            _width = value;
        }
    }

    public int Height
    {
        get => _height;
        set
        {
            if (value < MinHeight)
                throw new ArgumentOutOfRangeException("height");

            _height = value;
        }
    }

    public MazeCellType CellType { get; set; }
    public IMazeGridForm Form { get; set; }
    public IMazeGridGenerator GridGenerator { get; set; }

    public MazeSetup(MazeCellType cellType, IMazeGridForm form, IMazeGridGenerator gridGenerator)
    {
        _width = MinWidth;
        _height = MinHeight;
        CellType = cellType;
        Form = form;
        GridGenerator = gridGenerator;
    }
}
