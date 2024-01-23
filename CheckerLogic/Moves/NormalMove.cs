namespace CheckerLogic.Moves;

public class NormalMove : Move
{
    public override EMoveType Type => EMoveType.Normal;
    public override Position FromPosition { get; }
    public override Position ToPosition { get; }
    public override Position RemovePiece { get; }


    public NormalMove(Position fromPosition, Position toPosition,Position removePiece = null)
    {
        FromPosition = fromPosition;
        ToPosition = toPosition;
        RemovePiece = removePiece;
    }

    public override void ExecuteMove(Board board)
    {
        var piece = board[FromPosition];
        board[FromPosition] = null;
        board[ToPosition] = piece;
        if (RemovePiece!=null)
        {
            board[RemovePiece] = null;
        }
    }
    
}