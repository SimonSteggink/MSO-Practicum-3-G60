namespace UnitTests;
using Programming_Learning_App;

public class RepeatUntilTests
{
    [Fact]
    public async Task RepeatUntil_WallAhead_StopsBeforeWall()
    {
        CellType[,] cells = new CellType[1, 4];
        cells[0, 0] = CellType.Open;
        cells[0, 1] = CellType.Open;
        cells[0, 2] = CellType.Open;
        cells[0, 3] = CellType.Blocked;
        var commands = new List<ICommand>
        {
            new RepeatUntilCommand(Condition.WallAhead, new List<ICommand> { new MoveCommand(1) })
        };

        var result = await new BlockProgram("Test", commands).Execute(new Grid(1, 4, cells));

        Assert.Equal(2, result.FinalPosition.X);
    }

    [Fact]
    public async Task RepeatUntil_GridEdge_StopsAtEdge()
    {
        CellType[,] cells = new CellType[1, 3];
        var commands = new List<ICommand>
        {
            new RepeatUntilCommand(Condition.GridEdge, new List<ICommand> { new MoveCommand(1) })
        };

        var result = await new BlockProgram("Test", commands).Execute(new Grid(1, 3, cells));

        Assert.Equal(2, result.FinalPosition.X);
    }
}
