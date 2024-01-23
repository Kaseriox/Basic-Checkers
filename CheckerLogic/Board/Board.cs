using CheckerLogic.BoardPopulationStrategies;

namespace CheckerLogic;

public class Board 
{
    private const int BoardSize = 8;
    private readonly Piece[,] _pieces = new Piece[BoardSize , BoardSize];

    public Piece this[int x, int y]
    {
        get => _pieces[x, y];
        set => _pieces[x, y] = value;
    }

    public Piece this[Position pos]
    {
        get => this[pos.X, pos.Y];
        set => this[pos.X, pos.Y] = value;
    }

    public static Board Initialize(PopulationStrategy strategy)
    {
        var board = new Board();
        board = strategy.PopulateBoard(board);
        return board;
    }
    

    public static  bool IsInside(Position position)
    {
        return position.X is >= 0 and < BoardSize && position.Y is >= 0 and < BoardSize;
    }
    
    public bool IsEmpty(Position position)
    {
        return this[position] is null;
    }

    public static int GetBoardSize()
    {
        return BoardSize;
    }
    public bool BoardEdge(Position position,Axis axis)
    {
        return axis switch
        {
            Axis.X => position.X is BoardSize - 1 or 0,
            Axis.Y => position.Y is BoardSize - 1 or 0,
            _ => throw new ArgumentOutOfRangeException(nameof(axis), axis, null)
        };
    }

    public int CountPieces(Player player)
    {
        var pieces = 0;
        for (var i = 0; i < BoardSize; i++)
        {
            for (var j = 0; j < BoardSize; j++)
            {
              pieces+=CheckIfPiece(_pieces[i, j], player);
            }
        }
        return pieces;
    }

    private int CheckIfPiece(Piece piece, Player player)
    {
        return piece == null ? 0 : piece.Player == player ? 1 : 0;
    }
    
}
