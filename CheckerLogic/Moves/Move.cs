namespace CheckerLogic.Moves;

public abstract class Move : IEquatable<Move>
{
    public abstract EMoveType Type { get; }
    public abstract Position FromPosition { get; }
    public abstract Position ToPosition { get; }
    public abstract Position RemovePiece { get; }

    
    public abstract void ExecuteMove(Board board);

    public bool Equals(Move other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Type == other.Type && Equals(FromPosition, other.FromPosition) && Equals(ToPosition, other.ToPosition) && Equals(RemovePiece, other.RemovePiece);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Move)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)Type, FromPosition, ToPosition, RemovePiece);
    }

    public static bool operator ==(Move left, Move right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Move left, Move right)
    {
        return !Equals(left, right);
    }
}
