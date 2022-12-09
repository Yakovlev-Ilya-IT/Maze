using System;

public class HexMazeFormFactory : MazeFormFactory
{
    public override IMazeGridForm Get(MazeFormType form)
    {
        switch (form)
        {
            case MazeFormType.Rectangle:
                return new RectangleHexMazeForm();
            case MazeFormType.Ellipse:
                return new EllipseHexMazeForm();
            case MazeFormType.Triangle:
                return new TriangleHexMazeForm();
            case MazeFormType.Parallelogram:
                return new ParallelogramHexMazeForm();
            case MazeFormType.Ring:
                return new RingHexMazeForm();
        }

        throw new ArgumentException($"No maze form for {form}");
    }
}
