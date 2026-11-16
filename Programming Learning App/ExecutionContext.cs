using System.Windows.Controls;

namespace Programming_Learning_App;

public class ExecutionContext
{
    public Grid grid { get; }
    public List<string> Trace { get; }
    public event Action<Grid> OnExecutionStateChanged;

    public ExecutionContext(Grid grid)
    {
        this.grid = grid;
        Trace = new List<string>();
        this.grid.OnGridStateChanged += (updatedGrid) => OnExecutionStateChanged?.Invoke(updatedGrid);
    }
}
