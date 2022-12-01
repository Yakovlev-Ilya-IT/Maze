using System;

public class SquareMazeFormFactory : MazeFormFactory
{
    public override IMazeGridForm Get(MazeFormType form, int width, int height)
    {
        switch (form)
        {
            case MazeFormType.Rectangle:
                return new RectangleSquareMazeForm(width, height);
            case MazeFormType.Ellipse:
                return new EllipseSquareMazeForm(width, height);
            case MazeFormType.Triangle:
                return new TriangleSquareMazeForm(width, height);
            case MazeFormType.Parallelogram:
                return new ParallelogramSquareMazeForm(width, height);
            case MazeFormType.Ring:
                return new RingSquareMazeForm(width, height);
        }

        throw new ArgumentException($"No maze form for {form}");
    }
}
