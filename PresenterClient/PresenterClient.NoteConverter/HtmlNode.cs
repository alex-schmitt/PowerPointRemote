using System.Text;

namespace PresenterClient.NoteConverter
{
    public interface IHtmlNode
    {
        HtmlElement Parent { get; }
        void WriteHtml(StringBuilder stringBuilder);
    }
}