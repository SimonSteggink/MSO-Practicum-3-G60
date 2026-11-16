namespace Programming_Learning_App;


public struct ExecutionResult
{
    public List<string> Trace;
    public Position FinalPosition;
    public Direction FinalFacing;
}

public struct Position
{
    public int X;
    public int Y;
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
    public string Name;
    public List<ICommand> Commands;
    public event Action<Grid> OnProgramStateChanged;

    public BlockProgram(string name, List<ICommand> commands) 
    {
        Name = name;
        Commands = commands;
    }

    public async Task<ExecutionResult> Execute(Grid? grid = null, int delay = 0)
    {
        ExecutionContext context = new ExecutionContext(grid);
        context.OnExecutionStateChanged += (updatedGrid) => OnProgramStateChanged?.Invoke(updatedGrid);

        for (int i = 0; i < Commands.Count; i++)
        {
            Commands[i].Execute(context, delay);
        }

        ExecutionResult result = new ExecutionResult();
        result.Trace = context.Trace;
        result.FinalPosition = context.grid.character.Position;
        result.FinalFacing = context.grid.character.Facing;
        return result;
    }
}