using System.Collections.Generic;

namespace PresenterClient.NoteConverter
{
    public static class TagTable
    {
        public static Dictionary<Tag, string> TagToString = new Dictionary<Tag, string>
        {
            {Tag.Div, "div"},
            {Tag.P, "p"},
            {Tag.Ul, "ul"},
            {Tag.Ol, "ol"},
            {Tag.Li, "li"},
            {Tag.Br, "br"},
            {Tag.Strong, "strong"},
            {Tag.Em, "em"},
            {Tag.U, "u"},
            {Tag.Sub, "sub"},
            {Tag.Sup, "sup"},
            {Tag.S, "s"}
        };

        public static Dictionary<Tag, FormattingTag> TagToFormatting = new Dictionary<Tag, FormattingTag>
        {
            {Tag.Em, FormattingTag.Italic},
            {Tag.Strong, FormattingTag.Bold},
            {Tag.U, FormattingTag.Underline},
            {Tag.S, FormattingTag.Strikethrough},
            {Tag.Sub, FormattingTag.Subscript},
            {Tag.Sup, FormattingTag.Superscript}
        };

        public static Dictionary<FormattingTag, Tag> FormattingToTag = new Dictionary<FormattingTag, Tag>
        {
            {FormattingTag.Italic, Tag.Em},
            {FormattingTag.Bold, Tag.Strong},
            {FormattingTag.Underline, Tag.U},
            {FormattingTag.Strikethrough, Tag.S},
            {FormattingTag.Subscript, Tag.Sub},
            {FormattingTag.Superscript, Tag.Sup}
        };
    }
}