using CheckerLogic;
using CheckerLogic.BoardPopulationStrategies;
using CheckerLogic.Moves;
using Helpers;

namespace Tests;

public class Tests
{
    [Test]
    public void TestLegalMovesForPiece()
    {
        var gameState = new GameState(Board.Initialize(new CheckerStrategy()), Player.White);
        var piecePosition = new Position(5, 1);

        IEnumerable<Move> expectedLegalMoves = new List<Move>
        {
            new NormalMove(piecePosition, piecePosition + Direction.UpLeft),
            new NormalMove(piecePosition, piecePosition + Direction.UpRight)
        };
        
        var actualLegalMoves = gameState.LegalMovesForPiece(piecePosition).ToList();
        
        AssertLegalMoves(expectedLegalMoves,actualLegalMoves);
        return;

        void AssertLegalMoves(IEnumerable<Move> expected, IEnumerable<Move> actual)
        {
            Assert.That(actual.Count(), Is.EqualTo(expected.Count()), "Number of moves should match.");
        
            for (int i = 0; i < expected.Count(); i++)
            {
                AssertHelpers.EqualAssert(actual.ElementAt(i).ToPosition,expected.ElementAt(i).ToPosition);
                AssertHelpers.EqualAssert(actual.ElementAt(i).FromPosition,expected.ElementAt(i).FromPosition);
                AssertHelpers.EqualAssert(actual.ElementAt(i).RemovePiece,expected.ElementAt(i).RemovePiece);
            }
        }
    }

    [Test]
    public void TestStrategies()
    {
        var ExpectedBoard = Board.Initialize(new EmptyPopulationStrategy());
        var ActualBoard = Board.Initialize(new TestPopulationStrategy());
        
        AssertThatEntireBoardIsEmpty(ExpectedBoard);
        PopulateBoardAccordingToStrategy(ExpectedBoard);
        AssertThatBothBoardsAreEqual(ActualBoard,ExpectedBoard);
        return;
        
           void AssertThatEntireBoardIsEmpty(Board board)
            {
                ArrayHelper.IterateTrough2DArray(BoardPositionIsNull,Board.GetBoardSize(),Board.GetBoardSize(),board);
            }
            
            void PopulateBoardAccordingToStrategy(Board board)
            {
                board[1, 1] = new NormalPiece(Player.White);
                board[3,3]  = new NormalPiece(Player.Black);
            }
        
            void AssertThatBothBoardsAreEqual(Board actual, Board expected)
            {
                ArrayHelper.IterateTrough2DArray(BoardPositionIsEqual,Board.GetBoardSize(),Board.GetBoardSize(),expected,actual);
            }
            void BoardPositionIsNull(int i, int j,Board board)
            {
                AssertHelpers.EqualAssert(board[i,j],null);
            }
            void BoardPositionIsEqual(int i, int j,Board expected,Board actual)
            {
                AssertHelpers.EqualAssert(expected[i,j],actual[i,j]);
            }

    }

    

    [Test]
    public void TestKingPromotionAndTake()
    {
        var gameState = new GameState(Board.Initialize(new KingPromotionAndTakeStrategy()), Player.White);
        var expectedMove = new PromoteMove(new Position(2, 2), new Position(0, 0), new Position(1, 1));
        var actualMove = gameState.LegalMovesForPiece(new Position(2, 2)).Single(x => x.ToPosition == new Position(0,0));

        AssertHelpers.EqualAssert(actualMove,expectedMove);
    }

 
}

public class TestPopulationStrategy : PopulationStrategy
{
    private readonly Position _position1 = new(1, 1);
    private readonly Position _position2 = new(3, 3);
    public override Board PopulateBoard(Board board)
    {
        board[_position1] = new NormalPiece(Player.White);
        board[_position2] = new NormalPiece(Player.Black);
        return board;
    } 
}

public class EmptyPopulationStrategy : PopulationStrategy
{
    public override Board PopulateBoard(Board board)
    {
        return board;
    }
}

public class KingPromotionAndTakeStrategy : PopulationStrategy
{
    public override Board PopulateBoard(Board board)
    {
        board[1, 1] = new NormalPiece(Player.Black);
        board[2, 2] = new NormalPiece(Player.White);
        return board;
    }
}