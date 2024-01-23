namespace CheckerLogic;

public class Position : IEquatable<Position>, IComparable<Position>
{
    public int X { get; }
    public int Y { get; }

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
    public static bool operator ==(Position left, Position right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Position left, Position right)
    {
        return !Equals(left, right);
    }
    
    public static Position operator +(Position position, Direction direction)
    {
        return new Position(position.X + direction.DeltaX, position.Y + direction.DeltaY);
    }
    public static Position operator -(Position position, Direction direction)
    {
        return new Position(position.X - direction.DeltaX, position.Y - direction.DeltaY);
    }
    public int CompareTo(Position other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        var xComparison = X.CompareTo(other.X);
        return xComparison != 0 ? xComparison : Y.CompareTo(other.Y);
    } 
    public bool Equals(Position other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return X == other.X && Y == other.Y;
    }
     
         public override bool Equals(object obj)
         {
             if (ReferenceEquals(null, obj)) return false;
             if (ReferenceEquals(this, obj)) return true;
             return obj.GetType() == this.GetType() && Equals((Position)obj);
         }
}