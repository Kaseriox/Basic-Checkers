namespace CheckerLogic.Moves;

public class PromoteMove : Move
{
    public override EMoveType Type => EMoveType.Promote;
    public override Position FromPosition { get; }
    public override Position ToPosition { get; }
    public override Position RemovePiece { get; }

    public PromoteMove(Position fromPosition, Position toPosition, Position removePiece=null)
    {
        FromPosition = fromPosition;
        ToPosition = toPosition;
        RemovePiece = removePiece;
    }

    public override void ExecuteMove(Board board)
    {
        var piece = board[FromPosition];
        board[FromPosition] = null;
        board[ToPosition] = new KingPiece(piece.Player);
        if (RemovePiece!=null)
        {
            board[RemovePiece] = null;
        }
    }
}