using System.Collections.Generic;
using System.Text;
using PresenterClient.NoteConverter.ExtensionMethods;

namespace PresenterClient.NoteConverter
{
    public class HtmlElement : IHtmlNode
    {
        public HtmlElement(HtmlElement parent, Tag tag)
        {
            Parent = parent;
            Tag = tag;

            // Line breaks don't have additional properties 
            if (tag == Tag.Br) return;

            Children = new List<IHtmlNode>();
            InheritingStyle = new Styles();
            StyleAttribute = new Styles();

            var formattingTag = TagTable.TagToFormatting.ContainsKey(tag)
                ? TagTable.TagToFormatting[tag]
                : FormattingTag.None;


            // With html, all ancestor formatting tags are inherited.
            InheritingTags = parent?.InheritingTags | formattingTag ?? formattingTag;

            // With html, parent styles overwrite other ancestor styles.
            InheritingStyle.TextAlign =
                parent == null ? null : parent.StyleAttribute.TextAlign ?? parent.InheritingStyle.TextAlign;
            InheritingStyle.PaddingLeftEm = parent == null
                ? null
                : parent.StyleAttribute.PaddingLeftEm ?? parent.InheritingStyle.PaddingLeftEm;
            InheritingStyle.FontSizeEm = parent == null
                ? null
                : parent.StyleAttribute.FontSizeEm ?? parent.InheritingStyle.FontSizeEm;
        }

        /// <summary>
        ///     The encompassing tag of this element
        /// </summary>
        public Tag Tag { get; }

        /// <summary>
        ///     Formatting tags that any child node will inherit
        /// </summary>
        public FormattingTag InheritingTags { get; set; }

        /// <summary>
        ///     Children can be Text or Element nodes
        /// </summary>
        public List<IHtmlNode> Children { get; }

        /// <summary>
        ///     Represents the value in the element style attribute
        /// </summary>
        public Styles StyleAttribute { get; set; }

        /// <summary>
        ///     Style attributes that any child node will inherit unless overwritten
        /// </summary>
        public Styles InheritingStyle { get; set; }

        public HtmlElement Parent { get; }

        public void WriteHtml(StringBuilder stringBuilder)
        {
            var tag = TagTable.TagToString[Tag];

            stringBuilder.Append($"<{tag}{StyleAttribute.Value()}>");

            if (Tag == Tag.Br)
                return;

            foreach (var child in Children)
                child.WriteHtml(stringBuilder);

            stringBuilder.Append($"</{tag}>");
        }
    }
}