using System;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using PresenterClient.NoteConverter;

namespace PresenterClient.Services
{
    // Builds a data structure to assist with converting a slide shows slide into Html.
    public class NoteTreeBuilder
    {
        private readonly HtmlElement _root = new HtmlElement(null, Tag.Div);
        private readonly TextRange _textRange;

        public NoteTreeBuilder(TextRange textRange)
        {
            _textRange = textRange;
        }

        public HtmlElement Build()
        {
            HtmlElement currentList = null;
            var currentIndentLevel = 0;

            var paragraphCount = _textRange.Paragraphs().Count;

            for (var i = 1; i <= paragraphCount; i++)
            {
                var paragraphTextRange = _textRange.Paragraphs(i, 1);

                var list = ConvertList(paragraphTextRange.ParagraphFormat.Bullet.Type);

                /*
                 * Paragraph TextRanges that only consists of a line breaks need to be handled specially because <p></p> is not valid html.
                 * Also, you cannot have <br> or <p> tags as direct decedents of <ul> and <ol> elements.
                 */
                if (paragraphTextRange.Text == "\r")
                {
                    // The cleanest way to add a line break between list items, <ul><li>Hello<br><br></li><li>world</li></ul>
                    if (currentList != null && list != null)
                    {
                        // Guaranteed to be a HtmlElement because <ul> and <ol> can't anything except for <li>, <ul>, and <ol> elements.
                        var lastListItem = (HtmlElement) currentList.Children[currentList.Children.Count - 1];
                        var lastListItemChild = lastListItem.Children[lastListItem.Children.Count - 1];

                        // If it is the first line break after a list item, two line breaks are needed, otherwise just one is needed.
                        if (lastListItemChild is HtmlElement htmlElement && htmlElement.Tag == Tag.Br)
                        {
                            lastListItem.Children.Add(new HtmlElement(lastListItem, Tag.Br));
                        }
                        else
                        {
                            lastListItem.Children.Add(new HtmlElement(lastListItem, Tag.Br));
                            lastListItem.Children.Add(new HtmlElement(lastListItem, Tag.Br));
                        }

                        continue;
                    }

                    // Guaranteed to be a HtmlElement because the root should only contain <p>, <ul>, or <ol> elements.
                    // The best way to add a 
                    var previousParagraph = (HtmlElement) _root.Children[_root.Children.Count - 1];
                    previousParagraph.Children.Add(new HtmlElement(previousParagraph, Tag.Br));

                    continue;
                }


                var indentLevel = paragraphTextRange.IndentLevel - 1;

                // If previously paragraph was a list and current paragraph is not, exit the list by setting currentList to null
                if (list == null)
                    currentList = null;

                // In Html, new paragraphs start with the <p> element, unless it is a ordered/unordered list.
                HtmlElement currentElement;
                if (list == null)
                {
                    currentElement = new HtmlElement(_root, Tag.P);
                    _root.Children.Add(currentElement);
                }
                else
                {
                    // Start a new list
                    if (currentList == null || indentLevel > currentIndentLevel)
                    {
                        var parent = currentList ?? _root;
                        currentList = new HtmlElement(parent, list.GetValueOrDefault());
                        parent.Children.Add(currentList);
                    }
                    // Traverse up a list
                    else if (indentLevel < currentIndentLevel)
                    {
                        currentList = currentList.Parent;
                    }

                    // Add the list item
                    var listItem = new HtmlElement(currentList, Tag.Li);
                    currentList.Children.Add(listItem);
                    currentElement = listItem;
                }

                if (currentList == null && indentLevel != 0)
                {
                    if (currentElement.StyleAttribute == null)
                        currentElement.StyleAttribute = new Styles();

                    currentElement.StyleAttribute.TextIndentEm = (float?) (indentLevel * 2.5);
                }

                currentIndentLevel = indentLevel;

                for (var x = 1; x <= paragraphTextRange.Length; x++)
                {
                    var character = paragraphTextRange.Characters(x, 1);

                    if (character.Text == "\r")
                        break;

                    var characterFormattingTags = GetFormattingTags(character);

                    // Traverse up the Html tree, until the character can inherit styles and formatting tags
                    while (true)
                    {
                        // The character can inherit all of the currentInnerElement tags and doesn't have additional tags.
                        if (currentElement.InheritingTags == characterFormattingTags)
                        {
                            AddTextToElement(currentElement, character.Text);
                            break;
                        }

                        // The character can inherit all of the currentInnerElement tags and has additional tags.
                        if ((currentElement.InheritingTags | characterFormattingTags) == characterFormattingTags)
                        {
                            var additionalTags = characterFormattingTags ^ currentElement.InheritingTags;
                            currentElement = AddFormattingElements(currentElement, additionalTags);
                            AddTextToElement(currentElement, character.Text);
                            break;
                        }

                        // Traverse up
                        currentElement = currentElement.Parent;
                    }
                }
            }

            return _root;
        }

        /// <summary>
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="formattingTags"></param>
        /// <returns>The inner most element</returns>
        public static HtmlElement AddFormattingElements(HtmlElement parent, FormattingTag formattingTags)
        {
            var htmlElement = parent;

            foreach (FormattingTag formattingTag in Enum.GetValues(typeof(FormattingTag)))
            {
                if (!formattingTags.HasFlag(formattingTag) || formattingTag == FormattingTag.None) continue;

                var innerElement = new HtmlElement(htmlElement, TagTable.FormattingToTag[formattingTag]);
                htmlElement.Children.Add(innerElement);
                htmlElement = innerElement;
            }

            return htmlElement;
        }

        private static void AddTextToElement(HtmlElement element, string text)
        {
            var children = element.Children;

            if (children.Count > 0 && children[children.Count - 1] is HtmlText htmlText)
                htmlText.Value.Append(text);
            else
                children.Add(new HtmlText(element, text));
        }

        public static FormattingTag GetFormattingTags(TextRange character)
        {
            if (character.Count > 1)
                throw new InvalidOperationException(
                    $"{nameof(GetFormattingTags)} only works on a TextRange with one character.");

            var tags = FormattingTag.None;

            if (character.Font.Bold == MsoTriState.msoTrue)
                tags |= FormattingTag.Bold;
            if (character.Font.Italic == MsoTriState.msoTrue)
                tags |= FormattingTag.Italic;
            if (character.Font.Underline == MsoTriState.msoTrue)
                tags |= FormattingTag.Underline;
            if (character.Font.Subscript == MsoTriState.msoTrue)
                tags |= FormattingTag.Subscript;
            if (character.Font.Superscript == MsoTriState.msoTrue)
                tags |= FormattingTag.Superscript;

            return tags;
        }

        public static TextAlign? ConvertAlignment(PpParagraphAlignment ppAlignment)
        {
            switch (ppAlignment)
            {
                case PpParagraphAlignment.ppAlignLeft:
                    return null;
                case PpParagraphAlignment.ppAlignCenter:
                    return TextAlign.Center;
                case PpParagraphAlignment.ppAlignRight:
                    return TextAlign.Right;
                case PpParagraphAlignment.ppAlignJustify:
                    return TextAlign.Justify;
                default:
                    return null;
            }
        }

        public static Tag? ConvertList(PpBulletType ppBullet)
        {
            if (ppBullet == PpBulletType.ppBulletUnnumbered)
                return Tag.Ul;
            if (ppBullet == PpBulletType.ppBulletNumbered)
                return Tag.Ol;

            return null;
        }
    }
}