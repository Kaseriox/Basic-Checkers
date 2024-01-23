using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace CheckersScreen;

public class CheckerCursor
{
    public static readonly Cursor WhiteCursor = LoadCursor("Assets/CursorW.cur");
    public static readonly Cursor BlackCursor = LoadCursor("Assets/CursorB.cur");
    private static Cursor LoadCursor(string filepath)
    {
        var stream = Application.GetResourceStream(new Uri(filepath, UriKind.Relative))?.Stream;
        return new Cursor(stream, true);
    }
}