namespace CheckerLogic;

public class Result
{
    public Player Winner { get; }
    public EEndGameReason Reason { get; }

    public Result(Player winner, EEndGameReason reason)
    {
        Winner = winner;
        Reason = reason;
    }

    public static Result Win(Player player)
    {
        return new Result(player, EEndGameReason.Win);
    }

    public static Result Draw(EEndGameReason reason)
    {
        return new Result(Player.None, reason);
    }
}