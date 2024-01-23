using CheckerLogic.Moves;

namespace CheckerLogic;

public class KingPiece : Piece
{
    public override EPieceType Type => EPieceType.King;
    public override Player Player { get; }

    private readonly Direction[] _directions = {
        Direction.UpLeft, 
        Direction.UpRight, 
        Direction.DownLeft, 
        Direction.DownRight
    };
    public KingPiece(Player color)
    {
        Player = color;
    }

    public override IEnumerable<Move> GetMoves(Position fromPosition, Board board)
    {
        return Moves(fromPosition, board, _directions);
    }

    protected override IEnumerable<Move> Moves(Position fromPosition, Board board, IEnumerable<Direction> directions)
    {
        foreach (var direction in directions)
        {
            var to = fromPosition + direction;
            foreach (var move in NormalMove(fromPosition, board, to,false)) yield return move;
            foreach (var captureMove in CaptureMoves(fromPosition, board, to, direction,false)) yield return captureMove;
        }
    }
    
}