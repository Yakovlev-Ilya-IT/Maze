using UnityEngine;

public class MazeGridGeneratorFactory
{
    public IMazeGridGenerator Get(MazeCellType type)
    {
        IMazeGridGenerator mazeGridGenerator = GetGenerator(type);
        return mazeGridGenerator;
    }

    private IMazeGridGenerator GetGenerator(MazeCellType type)
    {
        switch (type)
        {
            case MazeCellType.Square:
                return new DepthFirstSearchMazeSquareGridGenerator();
            case MazeCellType.Hex:
                return new DepthFirstSearchMazeHexGridGenerator();
        }
        Debug.LogError($"No generator for {type}");
        return new DepthFirstSearchMazeSquareGridGenerator();
    }
}
