using System;
using System.Windows;
using CheckerLogic;

namespace CheckersScreen;

public partial class GameOverMenu
{
    public event Action<EChoice> SelectedChoice; 
    public GameOverMenu(GameState gameState)
    {
        InitializeComponent();
        SetResult(gameState);
    }

    private void SetResult(GameState gameState)
    {
        var result = gameState.Result;
        WinnerText.Text = GetWinnerText(result.Winner);
        ReasonText.Text = ReasonString(result.Reason, gameState.CurrentPlayer);
    }
    private void Restart_Click(object sender, RoutedEventArgs e)
    {
        SelectedChoice?.Invoke(EChoice.Restart);
    }

    private void Exit_Click(object sender, RoutedEventArgs e)
    {
        SelectedChoice?.Invoke(EChoice.Exit);
    }

    private static string GetWinnerText(Player winner)
    {
        return winner switch
        {
            Player.Black => "BLACK WON",
            Player.White => "WHITE WON",
            _ => "DRAW"
        };
    }

    private static string PlayerString(Player player)
    {
        return player switch
        {
            Player.White => "WHITE",
            Player.Black => "BLACK",
            _ => ""
        };
    }

    private static string ReasonString(EEndGameReason reason, Player player)
    {
        return reason switch
        {
            EEndGameReason.Win => $"WIN - {PlayerString(player)} DOESN'T HAVE PIECES",
            EEndGameReason.FiftyMoveRule => $"FIFTY MOVE RULE",
            _ => throw new ArgumentOutOfRangeException(nameof(reason), reason, null)
        };
    }
}