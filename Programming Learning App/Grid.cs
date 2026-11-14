namespace Programming_Learning_App;

public enum CellType {
    Blocked,
    Open,
    Goal
}

public class Grid
{
    int rows;
    int columns;
    CellType[,] grid;
    
    public Grid(int rows, int columns, CellType[,] grid) 
    {
        this.rows = rows;
        this.columns = columns;
        this.grid = grid;
    }
}