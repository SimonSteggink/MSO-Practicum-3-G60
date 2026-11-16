using System.Windows.Controls;

namespace ProgrammingLearningApp;

public class ExecutionContext
{
    public Grid Grid { get; }
    public List<string> Trace { get; }
    public event Action<Grid>? OnExecutionStateChanged;

    public ExecutionContext(Grid grid)
    {
        this.Grid = grid;
        Trace = new List<string>();
        this.Grid.OnGridStateChanged += (updatedGrid) => OnExecutionStateChanged?.Invoke(updatedGrid);
    }
}
