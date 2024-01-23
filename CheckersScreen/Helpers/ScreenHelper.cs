using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using CheckerLogic;
using CheckerLogic.Moves;
using Helpers;

namespace CheckersScreen.Helpers;

public sealed class ScreenHelper
{
    
    private Image[,] _pieceImages;
    private Rectangle[,] _highlights;
    private UniformGrid _highlightGrid;
    private UniformGrid _piecesGrid;
    private Grid _boardGrid;
    private static ScreenHelper _instance;
    
    public static ScreenHelper Instance => _instance ??= new ScreenHelper();

    public void InitializeGrids(int boardSize, UniformGrid highlightGrid,UniformGrid piecesGrid,Grid boardGrid)
    {
        _pieceImages = new Image[boardSize, boardSize];
        _highlights = new Rectangle[boardSize, boardSize];
        _highlightGrid = highlightGrid;
        _piecesGrid = piecesGrid;
        _boardGrid = boardGrid;
    }
    public void InitializeBoard()
    {
        ArrayHelper.IterateTrough2DArray(AddImagesAndHighlights,Board.GetBoardSize(),Board.GetBoardSize());
    }

    private void AddImagesAndHighlights(int i, int j)
    {
        AddImages(i, j);
        AddHighlights(i, j);
    }


    public void DrawBoard(Board board)
    {
        ArrayHelper.IterateTrough2DArray(SetPieceImage,Board.GetBoardSize(),Board.GetBoardSize(),board);
    }
    public Position GetSquarePosition(MouseButtonEventArgs e,int boardSize)
    {
        var point = e.GetPosition(_boardGrid);
        var squareSize = _boardGrid.ActualWidth / boardSize;
        var row =(int)(point.Y / squareSize);
        var col =(int)(point.X / squareSize);
        return new Position(row, col);
    }
    
    public static Cursor GetCursor(Player player)
    {
        return player switch
        {
            Player.White => CheckerCursor.WhiteCursor,
            Player.Black => CheckerCursor.BlackCursor,
            _ => throw new ArgumentOutOfRangeException(nameof(player), player, null)
        };
    }
    
    public void ShowMoves(Dictionary<Position,Move> moveCache)
    {
        var color = Colors.Teal;
        foreach (var to in moveCache.Keys)
        {
            _highlights[to.X, to.Y].Fill = new SolidColorBrush(color);
        }
    }
    public void HideMoves(Dictionary<Position,Move> moveCache)
    {
        foreach (var to in moveCache.Keys)
        {
            _highlights[to.X,to.Y].Fill = Brushes.Transparent;                
        }
    }
    
    private void SetPieceImage( int i, int j,Board board)
    {
        var piece = board[i, j];
        _pieceImages[i, j].Source = Images.GetImage(piece);
    } 
    private void AddHighlights(int i, int j)
    {
        var highlight = new Rectangle();
        _highlights[i, j] = highlight;
        _highlightGrid.Children.Add(highlight);
    }
     private void AddImages(int i, int j)
     {
         var image = new Image();
         _pieceImages[i, j] = image;
         _piecesGrid.Children.Add(image);
     }
}