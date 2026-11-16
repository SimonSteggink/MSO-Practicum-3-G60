namespace ProgrammingLearningApp;

public enum CellType {
    Blocked,
    Open,
    Goal,
    Character
}

public class Grid
{
    public int Rows { get; }
    public int Columns { get; }
    public CellType[,] Cells { get; }
    public Character Character { get; } = new Character();
    public event Action<Grid>? OnGridStateChanged;

    public Grid(int rows, int columns, CellType[,] cells)
    {
        Rows = rows;
        Columns = columns;
        Cells = cells;
    }

    public void MoveForward()
    {
        Position next = Character.NextPosition();
        if (next.X < 0 || next.X >= Columns || next.Y < 0 || next.Y >= Rows)
        {
            throw new InvalidOperationException("The character walked off the grid.");
        }
        if (Cells[next.Y, next.X] == CellType.Blocked)
        {
            throw new InvalidOperationException("The character walked into a blocked cell.");
        }

        Cells[Character.Position.Y, Character.Position.X] = CellType.Open;

        Character.MoveForward();

        Cells[Character.Position.Y, Character.Position.X] = CellType.Character;

        OnGridStateChanged?.Invoke(this);
    }
    public void Turn(TurnDirection turn)
    {
        Character.Turn(turn);

        OnGridStateChanged?.Invoke(this);
    }
}