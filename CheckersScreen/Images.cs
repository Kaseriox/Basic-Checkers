using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CheckerLogic;

namespace CheckersScreen;

public static class Images
{
    private static readonly Dictionary<EPieceType, ImageSource> WhiteSources = new()
    {
        {
            EPieceType.Normal, LoadImage("Assets/PawnW.png")
        },
        {
            EPieceType.King,LoadImage("Assets/KingW.png")
        }
    };
    private static readonly Dictionary<EPieceType, ImageSource> BlackSources = new()
    {
        {
            EPieceType.Normal, LoadImage("Assets/PawnB.png")
        },
        {
            EPieceType.King,LoadImage("Assets/KingB.png")
        }
    };
    private static ImageSource LoadImage(string filepath)
    {
        return new BitmapImage(new Uri(filepath, UriKind.Relative));
    }

    public static ImageSource GetImage(Player color, EPieceType type)
    {
        return color switch
        {
            Player.White => WhiteSources[type],
            Player.Black => BlackSources[type],
            _ => null
        };
    }

    public static ImageSource GetImage(Piece piece)
    {
        return piece == null ? null : GetImage(piece.Player, piece.Type);
    }
}