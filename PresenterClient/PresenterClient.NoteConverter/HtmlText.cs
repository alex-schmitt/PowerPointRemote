using System.Text;

namespace PresenterClient.NoteConverter
{
    public class HtmlText : IHtmlNode
    {
        public HtmlText(HtmlElement parent, string text = null)
        {
            Parent = parent;
            Value = new StringBuilder();

            if (text != null)
                Value.Append(text);
        }

        public StringBuilder Value { get; set; }
        public HtmlElement Parent { get; }

        public void WriteHtml(StringBuilder stringBuilder)
        {
            stringBuilder.Append(Value);
        }
    }
}