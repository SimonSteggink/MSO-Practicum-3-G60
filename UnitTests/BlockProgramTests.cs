namespace UnitTests;
using ProgrammingLearningApp;

public class BlockProgramTests
{
    // A grid without walls, so the tests don't depend on a level file
    private static Grid OpenGrid(int rows, int columns)
    {
        CellType[,] cells = new CellType[rows, columns];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                cells[i, j] = CellType.Open;
            }
        }

        return new Grid(rows, columns, cells);
    }

    [Fact]
    public async Task BlockProgram_ShouldReturnFinalPosition()
    {
        var commands = new List<ICommand>
        {
            new MoveCommand(2),
            new TurnCommand(TurnDirection.Right),
            new MoveCommand(1)
        };
        var blockProgram = new BlockProgram("Test1", commands);

        var result = await blockProgram.Execute(OpenGrid(3, 3));

        Assert.Equal(2, result.FinalPosition.X);
        Assert.Equal(1, result.FinalPosition.Y);
        Assert.Equal(Direction.South, result.FinalFacing);
    }

    [Fact]
    public async Task BlockProgram_ShouldTraceExecutedCommands()
    {
        var commands = new List<ICommand>
        {
            new MoveCommand(2),
            new TurnCommand(TurnDirection.Right)
        };
        var blockProgram = new BlockProgram("Test2", commands);

        var result = await blockProgram.Execute(OpenGrid(3, 3));

        Assert.Equal(new List<string> { "Move 2", "Turn right" }, result.Trace);
    }
}
