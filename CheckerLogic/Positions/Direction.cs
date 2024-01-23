namespace CheckerLogic;

public class Direction
{
    public static readonly Direction Left = new(0, -1);
    public static readonly Direction Right = new(0, 1);
    public static readonly Direction Up = new(-1, 0);
    public static readonly Direction Down = new(1, 0);

    public static readonly Direction UpLeft = Up + Left;
    public static readonly Direction UpRight = Up + Right;
    public static readonly Direction DownLeft = Down + Left;
    public static readonly Direction DownRight = Down + Right;
    
    public int  DeltaX { get; }
    public int DeltaY { get; }

    public Direction(int deltaX, int deltaY)
    {
        DeltaX = deltaX;
        DeltaY = deltaY;
    }

    public static Direction operator +(Direction direction1, Direction direction2)
    {
        return new Direction(direction1.DeltaX + direction2.DeltaX, direction1.DeltaY + direction2.DeltaY );
    }
    public static Direction operator *(Direction direction, int scale)
    {
        return new Direction(direction.DeltaX * scale, direction.DeltaY * scale);
    }
}