using System.Text;

namespace PresenterClient.NoteConverter.ExtensionMethods
{
    internal static class StylesExtensions
    {
        public static string Value(this Styles styles)
        {
            if (styles == null)
                return null;

            var builder = new StringBuilder();

            builder.Append(" style=\"");

            var startLength = builder.Length;

            if (styles.FontSizeEm != null)
                builder.Append($"font-size:{styles.FontSizeEm}em;");

            if (styles.TextIndentEm != null)
                builder.Append($"text-indent:{styles.TextIndentEm}em;");

            if (styles.TextAlign != null)
                builder.Append($"text-align:{TextAlignTable.Table[styles.TextAlign]};");

            if (builder.Length == startLength)
                return null;

            builder.Append("\"");

            return builder.ToString();
        }
    }
}