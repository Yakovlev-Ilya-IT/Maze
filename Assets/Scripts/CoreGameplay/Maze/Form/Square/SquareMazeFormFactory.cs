using System;

public class SquareMazeFormFactory : MazeFormFactory
{
    public override IMazeGridForm Get(MazeFormType form)
    {
        switch (form)
        {
            case MazeFormType.Rectangle:
                return new RectangleSquareMazeForm();
            case MazeFormType.Ellipse:
                return new EllipseSquareMazeForm();
            case MazeFormType.Triangle:
                return new TriangleSquareMazeForm();
            case MazeFormType.Parallelogram:
                return new ParallelogramSquareMazeForm();
            case MazeFormType.Ring:
                return new RingSquareMazeForm();
        }

        throw new ArgumentException($"No maze form for {form}");
    }
}
