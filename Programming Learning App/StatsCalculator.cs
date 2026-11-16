namespace ProgrammingLearningApp;


public struct ProgramStats {
    public int Commands { get; set; }
    public int MaxNesting { get; set; }
    public int Repeats { get; set; }
}
public static class StatsCalculator
{
    public static ProgramStats Calculate(BlockProgram p) 
    {
        ProgramStats stats = new ProgramStats();
        Visit(p.Commands, 0, ref stats);
        return stats;
    }

    // Walks the command tree, so commands inside repeat bodies are counted too
    private static void Visit(List<ICommand> commands, int depth, ref ProgramStats stats)
    {
        foreach (ICommand command in commands)
        {
            stats.Commands++;

            if (command is RepeatCommand repeat)
            {
                stats.Repeats++;
                stats.MaxNesting = Math.Max(stats.MaxNesting, depth + 1);
                Visit(repeat.Body, depth + 1, ref stats);
            }
            else if (command is RepeatUntilCommand repeatUntil)
            {
                stats.Repeats++;
                stats.MaxNesting = Math.Max(stats.MaxNesting, depth + 1);
                Visit(repeatUntil.Body, depth + 1, ref stats);
            }
        }
    }
}