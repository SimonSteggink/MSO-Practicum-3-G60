namespace ProgrammingLearningApp;


public struct ExecutionResult
{
    public List<string> Trace { get; set; }
    public Position FinalPosition { get; set; }
    public Direction FinalFacing { get; set; }
}

public struct Position
{
    public int X { get; set; }
    public int Y { get; set; }
}

public enum Direction
{
    North,
    East,
    South,
    West
}

public enum TurnDirection 
{
    Left,
    Right
}

public class BlockProgram
{
    public string Name { get; }
    public List<ICommand> Commands { get; }
    public event Action<Grid>? OnProgramStateChanged;

    public BlockProgram(string name, List<ICommand> commands) 
    {
        Name = name;
        Commands = commands;
    }

    public async Task<ExecutionResult> Execute(Grid grid, int delay = 0)
    {
        ExecutionContext context = new ExecutionContext(grid);
        context.OnExecutionStateChanged += (updatedGrid) => OnProgramStateChanged?.Invoke(updatedGrid);

        for (int i = 0; i < Commands.Count; i++)
        {
            Commands[i].Execute(context, delay);
        }

        ExecutionResult result = new ExecutionResult();
        result.Trace = context.Trace;
        result.FinalPosition = context.Grid.Character.Position;
        result.FinalFacing = context.Grid.Character.Facing;
        return result;
    }
}