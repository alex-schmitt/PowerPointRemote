using System;
using System.Collections.Generic;
using System.Text;

namespace PresenterClient.NoteConverter
{
    public static class HtmlWriter
    {
        private static ParagraphNode _previousParagraph;

        public static void WriteHtml(this RootNode rootNode, StringBuilder stringBuilder)
        {
            BeginTag(stringBuilder, TagTable.Block[BlockTag.Div]);
            foreach (var child in rootNode.Children)
                child.WriteHtml(stringBuilder);
            EndTag(stringBuilder, TagTable.Block[BlockTag.Div]);
        }

        public static void WriteHtml(this ParagraphNode paragraphNode, StringBuilder stringBuilder)
        {
            string openedTag = null;

            var closePreviousList = _previousParagraph != null && _previousParagraph.List != List.None &&
                                    _previousParagraph.List != paragraphNode.List;
            if (closePreviousList)
                EndTag(stringBuilder, TagTable.List[_previousParagraph.List]);

            if (paragraphNode.List == List.None && !paragraphNode.IsLineBreak)
            {
                BeginTag(stringBuilder, TagTable.Block[BlockTag.P]);
                openedTag = TagTable.Block[BlockTag.P];
            }
            else if (paragraphNode.IsLineBreak)
            {
                stringBuilder.Append("<br>");
            }
            else
            {
                if (_previousParagraph == null || _previousParagraph.List != paragraphNode.List)
                    BeginTag(stringBuilder, TagTable.List[paragraphNode.List]);

                BeginTag(stringBuilder, TagTable.Block[BlockTag.Li]);
                openedTag = TagTable.Block[BlockTag.Li];
            }

            foreach (var child in paragraphNode.Children)
                child.WriteHtml(stringBuilder);

            if (openedTag != null)
                EndTag(stringBuilder, openedTag);

            _previousParagraph = paragraphNode;
        }

        public static void WriteHtml(this StringNode stringNode, StringBuilder stringBuilder)
        {
            var openedTags = new List<string>();

            foreach (InlineTag tag in Enum.GetValues(stringNode.InlineTags.GetType()))
            {
                if (!stringNode.InlineTags.HasFlag(tag) || tag == InlineTag.None) continue;

                var tagValue = TagTable.Inline[tag];
                openedTags.Add(tagValue);
                BeginTag(stringBuilder, tagValue);
            }

            stringBuilder.Append(stringNode.InnerText);

            foreach (var child in stringNode.Children)
                child.WriteHtml(stringBuilder);

            EndTags(stringBuilder, openedTags);
        }

        private static void BeginTag(StringBuilder stringBuilder, string tag, string attributes = null)
        {
            stringBuilder.Append("<").Append(tag).Append(attributes).Append(">");
        }

        private static void EndTags(StringBuilder stringBuilder, IReadOnlyList<string> tags)
        {
            for (var i = tags.Count - 1; i >= 0; i--)
                EndTag(stringBuilder, tags[i]);
        }

        private static void EndTag(StringBuilder stringBuilder, string tag)
        {
            stringBuilder.Append("</").Append(tag).Append(">");
        }
    }
}