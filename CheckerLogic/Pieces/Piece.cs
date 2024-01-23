using CheckerLogic.Moves;

namespace CheckerLogic;

public abstract class Piece : IEquatable<Piece>
{
    public abstract EPieceType Type { get; }
    public abstract Player Player { get; }



    public abstract IEnumerable<Move> GetMoves(Position fromPosition, Board board);

    protected bool CanMoveTo(Position position, Board board)
    {
        return Board.IsInside(position) && board.IsEmpty(position);
    }

    protected bool CanCaptureAt(Position position, Board board)
    {
        if (!Board.IsInside(position) || board.IsEmpty(position))
        {
            return false;
        }
        return board[position].Player != Player;
    }

    protected abstract IEnumerable<Move> Moves(Position fromPosition, Board board, IEnumerable<Direction> directions);
    
    protected IEnumerable<Move> CaptureMoves(Position fromPosition, Board board, Position to, Direction direction,bool NormalPiece)
    {
        if (!CanCaptureAt(to, board)) yield break;
        var nextTo = to + direction;
        if(!CanMoveTo(nextTo,board)) yield break;
        if (board.BoardEdge(nextTo, Axis.X) && NormalPiece)
        {
            yield return new PromoteMove(fromPosition, nextTo, to);
        }
        else
        {
            yield return new NormalMove(fromPosition, nextTo, to);
        }
    }

    protected IEnumerable<Move> NormalMove(Position fromPosition, Board board, Position to,bool NormalPiece)
    {
        if (!CanMoveTo(to, board)) yield break;
        if (board.BoardEdge(to, Axis.X) && NormalPiece)
        {
            yield return new PromoteMove(fromPosition, to);
        }
        else
        {
            yield return new NormalMove(fromPosition, to);
        }
    }
    
    public bool Equals(Piece other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Type == other.Type && Player == other.Player;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Piece)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)Type, (int)Player);
    }

    public static bool operator ==(Piece left, Piece right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Piece left, Piece right)
    {
        return !Equals(left, right);
    }
}