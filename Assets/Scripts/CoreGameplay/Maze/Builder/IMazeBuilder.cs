public interface IMazeBuilder 
{
    IMazeBuilder SetMazeForm(MazeFormType form);
    IMazeBuilder SetSize(int width, int height);
    IMazeBuilder SetGenerationAlgoritm(MazeGenerationAlgorithm generationAlgorithm);
    Maze GetResult();
}
