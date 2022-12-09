public class SquareMazeSetupBuilder : MazeSetupBuilder
{
    protected override MazeCellType CellType => MazeCellType.Square;

    protected override MazeFormFactory GetFormFactory() => new SquareMazeFormFactory();

    protected override MazeGridGeneratorFactory GetGridGeneratorFactory() => new SquareMazeGridGeneratorFactory();
}
