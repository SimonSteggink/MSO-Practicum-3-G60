namespace Programming_Learning_App;

using System.IO;
using System.Windows;

public abstract class CommonUI
{
    protected BlockProgram? current;
    protected IProgramParser parser = new TextProgramParser();
    protected ExampleProgramProvider examples;
    protected StatsCalculator stats = new StatsCalculator();

    public CommonUI()
    {
        examples = new ExampleProgramProvider(parser);
    }

    public abstract void Run();

    protected void Execute()
    {
        if (current == null) { return; }

        ExecutionResult result = current!.Execute();
        string text = "";
        for (int i = 0; i < result.Trace.Count; i++)
        {
            if (i + 1 == result.Trace.Count)
            {
                text += result.Trace[i] + ".";
            }
            else
            {
                text += result.Trace[i] + ", ";
            }
        }

        Console.WriteLine(text);

        string facingText = "";
        switch (result.FinalFacing)
        {
            case Direction.North:
                facingText = "north";
                break;
            case Direction.East:
                facingText = "east";
                break;
            case Direction.South:
                facingText = "south";
                break;
            case Direction.West:
                facingText = "west";
                break;
        }

        Console.WriteLine("End state (" + result.FinalPosition.X + "," + result.FinalPosition.Y + ") facing " + facingText + ".");
    }

    protected void ShowMetrics()
    {
        ProgramStats result = stats.Calculate(current!);
        Console.WriteLine("No. of commands: " + result.Commands);
        Console.WriteLine("Max nesting: " + result.MaxNesting);
        Console.WriteLine("No. of repeats: " + result.Repeats);
    }
}
