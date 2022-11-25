using System;

public class HexMazeFormFactory : MazeFormFactory
{
    public override IMazeGridForm Get(MazeFormType form, int width, int height)
    {
        switch (form)
        {
            case MazeFormType.Rectangle:
                return new RectangleHexMazeForm(width, height);
            case MazeFormType.Ellipse:
                return new EllipseHexMazeForm(width, height);
            case MazeFormType.Triangle:
                return new TriangleHexMazeForm(width, height);
            case MazeFormType.Parallelogram:
                return new ParallelogramHexMazeForm(width, height);
            case MazeFormType.Ring:
                return new RingHexMazeForm(width, height);
        }

        throw new ArgumentException($"No maze form for {form}");
    }
}
