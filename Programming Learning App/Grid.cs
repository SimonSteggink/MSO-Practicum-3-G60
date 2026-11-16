namespace Programming_Learning_App;

public enum CellType {
    Blocked,
    Open,
    Goal,
    Character
}

public class Grid
{
    public int rows;
    public int columns;
    public CellType[,] grid;
    public Character character = new Character();
    public event Action<Grid> OnGridStateChanged;

    public Grid(int rows, int columns, CellType[,] grid) 
    {
        this.rows = rows;
        this.columns = columns;
        this.grid = grid;
    }

    public bool CheckCondition(Condition cond) 
    {
        Character tempChar = character;

        if (cond == Condition.WallAhead)
        {
            tempChar.MoveForward();

            if (grid[tempChar.Position.Y, tempChar.Position.X] == CellType.Blocked)
            {
                return true;
            }
        }
        else if (cond == Condition.GridEdge)
        {
            tempChar.MoveForward();

            if (tempChar.Position.Y > rows - 1) 
            {
                return true;
            }
            else if (tempChar.Position.X > columns - 1)
            {
                return true;
            }
        }
        return false;
    }

    public void MoveForward() 
    {
        grid[character.Position.Y, character.Position.X] = CellType.Open;

        character.MoveForward();

        grid[character.Position.Y, character.Position.X] = CellType.Character;


        OnGridStateChanged?.Invoke(this);
    }
    public void Turn(TurnDirection turn)
    {
        character.Turn(turn);

        OnGridStateChanged?.Invoke(this);
    }
}