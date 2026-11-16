namespace ProgrammingLearningApp;

using System.IO;
using System.Windows;
using System.Windows.Controls;

public abstract class CommonUI
{
    protected BlockProgram? Current { get; set; }
    protected IProgramParser Parser { get; } = new TextProgramParser();
    protected IGridParser GridParser { get; } = new TextGridParser();
    protected ExampleProgramProvider Examples { get; } = new ExampleProgramProvider();
    protected Grid WorldGrid { get; set; }
    protected string FilePath { get; set; } = "Test.txt";

    public CommonUI()
    {
        WorldGrid = LoadGrid();
    }

    private Grid LoadGrid()
    {
        string gridText = File.ReadAllText(FilePath);
        return GridParser.Parse(gridText);
    }

    public abstract void Run();

    protected async Task<string> Execute(int delay)
    {
        if (Current == null) { return ""; }

        WorldGrid = LoadGrid();

        ExecutionResult result = await Current!.Execute(WorldGrid, delay);
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

        return text + "\n" + "End state (" + result.FinalPosition.X + "," + result.FinalPosition.Y + ") facing " + facingText + ".";
    }

    protected string ShowMetrics()
    {
        ProgramStats result = StatsCalculator.Calculate(Current!);
        return "No. of commands: " + result.Commands + "\n" +
               "Max nesting: " + result.MaxNesting + "\n" +
               "No. of repeats: " + result.Repeats;
    }
}
