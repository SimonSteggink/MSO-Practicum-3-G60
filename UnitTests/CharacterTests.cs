namespace UnitTests;
using ProgrammingLearningApp;

public class CharacterTests
{
    [Fact]
    public void Character_ShouldMoveEastByDefault()
    {
        var character = new ProgrammingLearningApp.Character();

        character.MoveForward();

        Assert.Equal(1, character.Position.X);
        Assert.Equal(0, character.Position.Y);
    }

    [Fact]
    public void Character_ShouldTurnLeftFromEastToNorth()
    {
        var character = new ProgrammingLearningApp.Character();

        character.Turn(TurnDirection.Left);

        Assert.Equal(Direction.North, character.Facing);
    }
}
