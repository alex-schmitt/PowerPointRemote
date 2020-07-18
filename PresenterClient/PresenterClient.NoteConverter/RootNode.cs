using System.Collections.Generic;

namespace PresenterClient.NoteConverter
{
    public class RootNode
    {
        public RootNode()
        {
            Children = new List<ParagraphNode>();
        }

        public List<ParagraphNode> Children { get; }
    }
}