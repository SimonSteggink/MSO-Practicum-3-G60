namespace Programming_Learning_App;

using System.IO;
using System.Windows;

public class GUI
{
    private BlockProgram? current;
    private IProgramParser parser = new TextProgramParser();
    private ExampleProgramProvider examples;
    private StatsCalculator stats = new StatsCalculator();

    public GUI()
    {
        examples = new ExampleProgramProvider(parser);
    }

    [STAThread]
    public void Run()
    {
        Thread wpfThread = new Thread(() =>
        {
            var app = new Application();
            var window = new Window1();
            app.Run(window);
        });

        wpfThread.SetApartmentState(ApartmentState.STA);
        wpfThread.Start();
        wpfThread.Join();
    }

    private void Execute()
    {
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

    private void ShowMetrics()
    {
        ProgramStats result = stats.Calculate(current!);
        Console.WriteLine("No. of commands: " + result.Commands);
        Console.WriteLine("Max nesting: " + result.MaxNesting);
        Console.WriteLine("No. of repeats: " + result.Repeats);
    }
}
