using System;

public class SquareMazeGridGeneratorFactory : MazeGridGeneratorFactory
{
    public override IMazeGridGenerator Get(MazeGenerationAlgorithm type)
    {
        switch (type)
        {
            case MazeGenerationAlgorithm.DepthFirstSearch:
                return new DepthFirstSearchMazeSquareGridGenerator();
        }

        throw new ArgumentException($"No generation algoritm for {type}");
    }
}
