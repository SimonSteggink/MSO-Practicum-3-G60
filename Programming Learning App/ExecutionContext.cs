namespace Programming_Learning_App;

public class ExecutionContext
{
    public Character Character { get; }
    public List<string> Trace { get; }
    public Grid? Grid { get; }

    public ExecutionContext(Grid? grid = null)
    {
        Character = new Character();
        Trace = new List<string>();
        Grid = grid;
    }
}
