using CheckerLogic.Moves;

namespace CheckerLogic;

public sealed class GameState
{
    public Board Board { get; }
    public Player CurrentPlayer { get; private set; }

    public Result Result { get; set; }
    public GameState(Board board, Player currentPlayer)
    {
        Board = board;
        CurrentPlayer = currentPlayer;
    }

    public IEnumerable<Move> LegalMovesForPiece(Position position)
    {
        if (Board.IsEmpty(position) || Board[position].Player != CurrentPlayer)
        {
            return Enumerable.Empty<Move>();
        }

        var piece = Board[position];
        return piece.GetMoves(position, Board);
    }

    public void MakeMove(Move move)
    {
        move.ExecuteMove(Board);
        CurrentPlayer = CurrentPlayer.Opponent();
        CheckIfGameIsOver(CurrentPlayer);
    }
    public bool IsGameOver()
    {
        return Result is not null;
    }

    private void CheckIfGameIsOver(Player player)
    {
        if (Board.CountPieces(player) == 0)
        {
            Result = Result.Win(player.Opponent());
        }
    }
}