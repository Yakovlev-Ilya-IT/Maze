public class HexMazeSetupBuilder : MazeSetupBuilder
{
    protected override MazeCellType CellType => MazeCellType.Hex;

    protected override MazeFormFactory GetFormFactory() => new HexMazeFormFactory();

    protected override MazeGridGeneratorFactory GetGridGeneratorFactory() => new HexMazeGridGeneratorFactory();
}
