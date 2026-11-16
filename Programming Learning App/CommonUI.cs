namespace Programming_Learning_App;

using System.IO;
using System.Windows;
using System.Windows.Controls;

public abstract class CommonUI
{
    protected BlockProgram? current;
    protected IProgramParser parser = new TextProgramParser();
    protected IGridParser gridParser = new TextGridParser();
    protected ExampleProgramProvider examples;
    protected StatsCalculator stats = new StatsCalculator();
    protected Grid worldGrid;

    public CommonUI()
    {
        examples = new ExampleProgramProvider();
        InitiateGrid();
    }

    private void InitiateGrid() 
    {
        string gridText = File.ReadAllText("Test.txt");
        worldGrid = gridParser.Parse(gridText);
    }

    public abstract void Run();

    protected async Task<string> Execute(int delay)
    {
        if (current == null) { return ""; }

        InitiateGrid();

        ExecutionResult result = await current!.Execute(worldGrid, delay);
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
        ProgramStats result = stats.Calculate(current!);
        return "No. of commands: " + result.Commands + "\n" +
               "Max nesting: " + result.MaxNesting + "\n" +
               "No. of repeats: " + result.Repeats;
    }
}
