using System.Collections.Generic;

namespace PresenterClient.NoteConverter
{
    public static class TagTable
    {
        static TagTable()
        {
            Inline = CreateInlineTagTable();
            Block = CreateBlockTagTable();
            List = CreateListTagTable();
        }

        public static Dictionary<InlineTag, string> Inline { get; }
        public static Dictionary<BlockTag, string> Block { get; }
        public static Dictionary<List, string> List { get; }

        private static Dictionary<InlineTag, string> CreateInlineTagTable()
        {
            return new Dictionary<InlineTag, string>
            {
                {InlineTag.Bold, "strong"},
                {InlineTag.Italic, "em"},
                {InlineTag.Underline, "u"},
                {InlineTag.Subscript, "sub"},
                {InlineTag.Superscript, "sup"},
                {InlineTag.Strikethrough, "s"}
            };
        }

        private static Dictionary<BlockTag, string> CreateBlockTagTable()
        {
            return new Dictionary<BlockTag, string>
            {
                {BlockTag.Div, "div"},
                {BlockTag.P, "p"},
                {BlockTag.Ul, "ul"},
                {BlockTag.Ol, "ol"},
                {BlockTag.Li, "li"},
                {BlockTag.Br, "br"}
            };
        }

        private static Dictionary<List, string> CreateListTagTable()
        {
            return new Dictionary<List, string>
            {
                {NoteConverter.List.Ordered, Block[BlockTag.Ol]},
                {NoteConverter.List.Bullet, Block[BlockTag.Ul]}
            };
        }
    }
}