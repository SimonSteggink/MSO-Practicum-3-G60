namespace Programming_Learning_App;

public interface ICommand
{
    public void Execute(ExecutionContext context, int delay = 0);
}

public class MoveCommand : ICommand
{
    public int Steps;

    public MoveCommand(int steps)
    {
        Steps = steps;
    }

    public void Execute(ExecutionContext context, int delay = 0)
    {
        for (int i = 0; i < Steps; i++)
        {
            context.Grid.MoveForward();

            if (delay > 0)
            {
                Thread.Sleep(delay);
            }
        }

        context.Trace.Add("Move " + Steps);
    }
}

public class TurnCommand : ICommand
{
    public TurnDirection Turn;

    public TurnCommand(TurnDirection turn)
    {
        Turn = turn;
    }

    public void Execute(ExecutionContext context, int delay = 0)
    {
        context.Grid.Turn(Turn);

        if (Turn == TurnDirection.Left)
        {
            context.Trace.Add("Turn left");
        }
        else
        {
            context.Trace.Add("Turn right");
        }

        if (delay > 0)
        {
            Thread.Sleep(delay);
        }
    }
}

public class RepeatCommand : ICommand
{
    public int Times;
    public List<ICommand> Body;

    public RepeatCommand(int count, List<ICommand> commands)
    {
        Times = count;
        Body = commands;
    }

    public void Execute(ExecutionContext context, int delay = 0)
    {
        for (int i = 0; i < Times; i++)
        {
            for (int j = 0; j < Body.Count; j++)
            {
                Body[j].Execute(context, delay);
            }
        }
    }
}

public enum Condition {
    WallAhead,
    GridEdge
}

public class RepeatUntilCommand : ICommand
{
    public Condition condition;
    public List<ICommand> Body;

    public RepeatUntilCommand(Condition cond, List<ICommand> commands)
    {
        condition = cond;
        Body = commands;
    }

    public void Execute(ExecutionContext context, int delay)
    {
        // Capped so a missing grid (condition always false) can't loop forever
        for (int i = 0; i < 1000 && !IsMet(context); i++)
        {
            for (int j = 0; j < Body.Count; j++)
            {
                Body[j].Execute(context, delay);
            }
        }
    }

    private bool IsMet(ExecutionContext context)
    {
        Grid? grid = context.Grid;
        if (grid == null) { return false; }

        Position next = context.Grid.character.Position;
        if (context.Grid.character.Facing == Direction.North) { next.Y--; }
        if (context.Grid.character.Facing == Direction.East) { next.X++; }
        if (context.Grid.character.Facing == Direction.South) { next.Y++; }
        if (context.Grid.character.Facing == Direction.West) { next.X--; }

        bool inside = next.X >= 0 && next.X < grid.columns && next.Y >= 0 && next.Y < grid.rows;

        if (condition == Condition.GridEdge) { return !inside; }
        return inside && grid.grid[next.Y, next.X] == CellType.Blocked;
    }
}
