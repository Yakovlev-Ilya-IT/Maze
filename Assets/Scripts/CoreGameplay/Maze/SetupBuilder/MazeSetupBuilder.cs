public abstract class MazeSetupBuilder
{
    private MazeSetup _setup;

    private const MazeGenerationAlgorithm DefaultGenerationAlgorithm = MazeGenerationAlgorithm.DepthFirstSearch;
    private const MazeFormType DefaultForm = MazeFormType.Rectangle;
    protected abstract MazeCellType CellType { get; }

    private MazeGridGeneratorFactory _gridGeneratorFactory;
    private MazeFormFactory _formFactory;

    public MazeSetupBuilder()
    {
        _gridGeneratorFactory = GetGridGeneratorFactory();
        _formFactory = GetFormFactory();

        _setup = MakeDefaultConfig();
    }

    protected abstract MazeFormFactory GetFormFactory();
    protected abstract MazeGridGeneratorFactory GetGridGeneratorFactory();
    protected MazeSetup MakeDefaultConfig() => new MazeSetup(CellType, _formFactory.Get(DefaultForm), _gridGeneratorFactory.Get(DefaultGenerationAlgorithm));

    public MazeSetup GetResult()
    {
        MazeSetup config = _setup;

        _setup = MakeDefaultConfig();

        return config;
    }

    public MazeSetupBuilder SetGenerationAlgoritm(MazeGenerationAlgorithm generationAlgorithm)
    {
        _setup.GridGenerator = _gridGeneratorFactory.Get(generationAlgorithm);

        return this;
    }

    public MazeSetupBuilder SetMazeForm(MazeFormType form)
    {
        _setup.Form = _formFactory.Get(form);

        return this;
    }

    public MazeSetupBuilder SetSize(int width, int height)
    {
        _setup.Width = width;
        _setup.Height = height;

        return this;
    }
}
