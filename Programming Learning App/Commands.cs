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
            context.grid.MoveForward();

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
        context.grid.Turn(Turn);

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

    public void Execute(ExecutionContext context, int delay = 0)
    {
        while (context.grid.CheckCondition(condition)) 
        {
            for (int j = 0; j < Body.Count; j++)
            {
                Body[j].Execute(context, delay);
            }
        }
    }
}
