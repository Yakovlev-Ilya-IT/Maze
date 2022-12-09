using System;

public class MazeSetupBuilderFactory
{
    public MazeSetupBuilder Get(MazeCellType cellType)
    {
        switch (cellType)
        {
            case MazeCellType.Square:
                return new SquareMazeSetupBuilder();
            case MazeCellType.Hex:
                return new HexMazeSetupBuilder();
            default:
                throw new ArgumentException("Error cell type");
        }
    }
}
