using System.Collections.Generic;

namespace PresenterClient.NoteConverter
{
    public class ParagraphNode
    {
        public ParagraphNode()
        {
            Children = new List<StringNode>();
        }

        public List<StringNode> Children { get; }
        public List List { get; set; }
        public int Indent { get; set; }
        public Alignment Alignment { get; set; }
        public bool IsLineBreak { get; set; }
    }
}