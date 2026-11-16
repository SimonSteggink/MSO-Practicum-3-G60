namespace ProgrammingLearningApp;

public interface ICommand
{
    public void Execute(ExecutionContext context, int delay = 0);
}

public class MoveCommand : ICommand
{
    public int Steps { get; }

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
    public TurnDirection Turn { get; }

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
    public int Times { get; }
    public List<ICommand> Body { get; }

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
    public Condition Condition { get; }
    public List<ICommand> Body { get; }

    public RepeatUntilCommand(Condition cond, List<ICommand> commands)
    {
        Condition = cond;
        Body = commands;
    }

    public void Execute(ExecutionContext context, int delay)
    {
        // Capped so a condition that never becomes true can't loop forever
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
        Grid grid = context.Grid;

        Position next = grid.Character.Position;
        if (grid.Character.Facing == Direction.North) { next.Y--; }
        if (grid.Character.Facing == Direction.East) { next.X++; }
        if (grid.Character.Facing == Direction.South) { next.Y++; }
        if (grid.Character.Facing == Direction.West) { next.X--; }

        bool inside = next.X >= 0 && next.X < grid.Columns && next.Y >= 0 && next.Y < grid.Rows;

        if (Condition == Condition.GridEdge) { return !inside; }
        return inside && grid.Cells[next.Y, next.X] == CellType.Blocked;
    }
}
