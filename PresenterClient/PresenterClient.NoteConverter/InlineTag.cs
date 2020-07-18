using System;

namespace PresenterClient.NoteConverter
{
    [Flags]
    public enum InlineTag
    {
        None = 0,
        Bold = 1,
        Italic = 2,
        Underline = 4,
        Subscript = 8,
        Superscript = 16,
        Strikethrough = 32
    }
}