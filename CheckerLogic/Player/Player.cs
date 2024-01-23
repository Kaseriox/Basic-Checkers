namespace CheckerLogic;

public enum Player
{
    White,
    Black,
    None
}

public static class PlayerExtensions
{
    public static Player Opponent(this Player player)
    {
        return player switch
        {
            Player.White => Player.Black,
            Player.Black => Player.White,
            _ => Player.None
        };
    }
}