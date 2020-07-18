using System.Collections.Generic;
using System.Text;

namespace PresenterClient.NoteConverter
{
    public class StringNode
    {
        public StringNode(StringNode parent)
        {
            Parent = parent;
            Children = new List<StringNode>();
            InnerText = new StringBuilder();
        }

        public StringNode Parent { get; set; }
        public float FontSize { get; set; }
        public InlineTag InlineTags { get; set; }
        public int FontColor { get; set; }
        public int HighlightColor { get; set; }

        public List<StringNode> Children { get; }
        public StringBuilder InnerText { get; }
    }
}