using System;

public class HexMazeGridGeneratorFactory : MazeGridGeneratorFactory
{
    public override IMazeGridGenerator Get(MazeGenerationAlgorithm type)
    {
        switch (type)
        {
            case MazeGenerationAlgorithm.DepthFirstSearch:
                return new DepthFirstSearchMazeHexGridGenerator();
        }

        throw new ArgumentException($"No generation algoritm for {type}");
    }
}
