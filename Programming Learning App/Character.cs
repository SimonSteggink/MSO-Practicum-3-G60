namespace ProgrammingLearningApp;

public class Character
{
    private Position position;
    public Direction Facing { get; private set; }
    public List<Position> Path { get; }

    public Position Position => position;

    public Character() {
        Facing = Direction.East;
        Path = new List<Position>();
    }

    public void Turn(TurnDirection dir) 
    {
        if (dir == TurnDirection.Left) 
        {
            switch (Facing)
            {
                case Direction.North:
                    Facing = Direction.West;
                    break;
                case Direction.East:
                    Facing = Direction.North;
                    break;
                case Direction.South:
                    Facing = Direction.East;
                    break;
                case Direction.West:
                    Facing = Direction.South;
                    break;
            }
        }
        else 
        {
            switch (Facing)
            {
                case Direction.North:
                    Facing = Direction.East;
                    break;
                case Direction.East:
                    Facing = Direction.South;
                    break;
                case Direction.South:
                    Facing = Direction.West;
                    break;
                case Direction.West:
                    Facing = Direction.North;
                    break;
            }
        }
    }

    // The cell the character would step onto if it moved forward now
    public Position NextPosition()
    {
        Position next = position;
        switch (Facing)
        {
            case Direction.North:
                next.Y--;
                break;
            case Direction.East:
                next.X++;
                break;
            case Direction.South:
                next.Y++;
                break;
            case Direction.West:
                next.X--;
                break;
        }

        return next;
    }

    public void MoveForward()
    {
        position = NextPosition();
        Path.Add(position);
    }
}