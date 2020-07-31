using System;

namespace PresenterClient.NoteConverter
{
    public class Styles : IEquatable<Styles>
    {
        public float? FontSizeEm { get; set; }
        public float? TextIndentEm { get; set; }
        public TextAlign? TextAlign { get; set; }

        public bool Equals(Styles other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Nullable.Equals(FontSizeEm, other.FontSizeEm) &&
                   Nullable.Equals(TextIndentEm, other.TextIndentEm) && TextAlign == other.TextAlign;
        }
    }
}