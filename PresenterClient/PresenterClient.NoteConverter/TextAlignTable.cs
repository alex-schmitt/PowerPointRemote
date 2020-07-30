using System.Collections.Generic;

namespace PresenterClient.NoteConverter
{
    public static class TextAlignTable
    {
        public static readonly Dictionary<TextAlign?, string> Table = new Dictionary<TextAlign?, string>
        {
            {TextAlign.Center, "Center"},
            {TextAlign.Right, "Right"},
            {TextAlign.Justify, "Justify"}
        };
    }
}