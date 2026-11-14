using System.Diagnostics;

namespace Programming_Learning_App;

public interface IGridParser
{
    public Grid Parse(string text);
}

public class TextGridParser : IGridParser
{
    public Grid Parse(string text) 
    {
        string[] textList = text.Replace("\r", "").Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

        int rows = textList.Length;
        int cols = textList[0].Length;

        CellType[,] grid = new CellType[rows, cols];

        for (int i = 0; i < rows; i++) 
        {
            for (int j = 0; j < cols; j++) 
            {
                if (textList[i][j] == 'o') 
                {
                    grid[i, j] = CellType.Open;
                }
                else if (textList[i][j] == '+')
                {
                    grid[i, j] = CellType.Blocked;
                }
                else if (textList[i][j] == 'x')
                {
                    grid[i, j] = CellType.Goal;
                }
            }
        }

        return new Grid(rows, cols, grid);
    }
}