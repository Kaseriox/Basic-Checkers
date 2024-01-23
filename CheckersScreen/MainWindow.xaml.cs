using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CheckerLogic;
using CheckerLogic.BoardPopulationStrategies;
using CheckerLogic.Moves;
using CheckersScreen.Helpers;

namespace CheckersScreen
{
    public partial class MainWindow
    {
        private readonly ScreenHelper _screenHelper = ScreenHelper.Instance;
        private readonly Dictionary<Position, Move> _moveCache = new();
        private readonly int _boardSize = Board.GetBoardSize();
        private GameState _gameState;
        private Position _selectedPosition;
        public MainWindow()
        {
            InitializeComponent();
            InitializeScreenHelper();
            InitializeBoard();
            InitializeGame();
        }

        private void InitializeScreenHelper()
        {
            _screenHelper.InitializeGrids(_boardSize,HighlightGrid,PiecesGrid,BoardGrid);
        }
        private void InitializeGame()
        {
            _gameState = new GameState(Board.Initialize(new CheckerStrategy()), Player.White);
            DrawBoardAndCursor();
        }

        private void DrawBoardAndCursor()
        {
            _screenHelper.DrawBoard(_gameState.Board);
            SetCursor(_gameState.CurrentPlayer);
        }

        private void InitializeBoard()
        {
            _screenHelper.InitializeBoard();
        }
        private void CacheMoves(IEnumerable<Move> moves)
        {
            _moveCache.Clear();
            foreach (var move in moves)
            {
                _moveCache[move.ToPosition] = move;
            }
        }
        private void BoardGrid_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsMenuOnScreen())
            {
                return;
            }
            var position = GetSquarePosition(e,_boardSize);
            if (_selectedPosition == null)
            {
                FromSelectedPosition(position);
            }
            else
            {
                ToSelectedPosition(position);
            }
        }

        private void FromSelectedPosition(Position position)
        {
            _selectedPosition = position;
            
            var moves = _gameState.LegalMovesForPiece(position);
            var enumerable = moves as Move[] ?? moves.ToArray();
            if (!enumerable.Any()) return;
            
            CacheMoves(enumerable);
            _screenHelper.ShowMoves(_moveCache);
        }

        private void ToSelectedPosition(Position position)
        {
            _selectedPosition = null;
            _screenHelper.HideMoves(_moveCache);
            if (!_moveCache.TryGetValue(position, out var move)) return;
            HandleMove(move);
        }

        private void HandleMove(Move move)
        {
            _gameState.MakeMove(move);
            DrawBoardAndCursor();
            if (_gameState.IsGameOver())
            {
                ShowGameOver();
            }
        }
        private Position GetSquarePosition(MouseButtonEventArgs e,int boardSize)
        {
           return _screenHelper.GetSquarePosition(e,boardSize);
        }

        private void SetCursor(Player player)
        {
            Cursor = ScreenHelper.GetCursor(player);
        }

        private bool IsMenuOnScreen()
        {
            return MenuContainer.Content is not null;
        }

        private void ShowGameOver()
        {
            var gameOverMenu = new GameOverMenu(_gameState);
            MenuContainer.Content = gameOverMenu;

            gameOverMenu.SelectedChoice += option =>
            {
                switch (option)
                {
                    case EChoice.Restart:
                        MenuContainer.Content = null;
                        RestartGame();
                        break;
                    case EChoice.Exit:
                        Application.Current.Shutdown();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(option), option, null);
                }
            };
        }

        private void RestartGame()
        {
            _screenHelper.HideMoves(_moveCache);
            _moveCache.Clear();
            InitializeGame();
            
        }
    }
}