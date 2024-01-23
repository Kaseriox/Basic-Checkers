using CheckerLogic.Moves;

namespace CheckerLogic;

public class NormalPiece : Piece
{
    public override EPieceType Type => EPieceType.Normal;
    public override Player Player { get; }

    private readonly Direction[] _forwardDirections = new Direction[2];
    
    public NormalPiece(Player player)
    {
        Player = player;
        switch (player)
        {
            case Player.White:
                _forwardDirections[0] = Direction.UpLeft;
                _forwardDirections[1] = Direction.UpRight;
                break;
            case Player.Black:
                _forwardDirections[0] = Direction.DownLeft; 
                _forwardDirections[1] = Direction.DownRight;
                break;
            case Player.None:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(player), player, null);
        }
        
    }

    public override IEnumerable<Move> GetMoves(Position fromPosition, Board board)
    {
        return Moves(fromPosition, board,_forwardDirections);
    }
    protected override IEnumerable<Move> Moves(Position fromPosition, Board board, IEnumerable<Direction> directions)
    {
        foreach (var direction in directions)
        {
            var to = fromPosition + direction;
            foreach (var move in NormalMove(fromPosition, board, to,true)) yield return move;
            foreach (var move1 in CaptureMoves(fromPosition, board, to, direction,true)) yield return move1;
        }
    }

   
}