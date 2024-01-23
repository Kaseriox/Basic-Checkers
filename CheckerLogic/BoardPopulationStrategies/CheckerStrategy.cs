using Helpers;

namespace CheckerLogic.BoardPopulationStrategies;

public class CheckerStrategy : PopulationStrategy
{
    public override Board PopulateBoard(Board board)
    {
        ArrayHelper.IterateTrough2DArray(SelectPiece,Board.GetBoardSize(),Board.GetBoardSize(),board);
        return board;
    }

    private void SelectPiece(int i, int j,Board board )
    {
        if (IsEven(i + j))
            board[i, j] = i switch
            {
                < 3 => CreatePiece(Player.Black),
                > 4 => CreatePiece(Player.White),
                _ => board[i, j]
            };
    }

    private static bool IsEven(int number)
    {
        return (number & 1) == 0;
    }
    private NormalPiece CreatePiece(Player player)
    {
        return new NormalPiece(player);
    }
}