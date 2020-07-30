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
            var currentInnerElement = _root;

            for (var i = 1; i <= _textRange.Length; i++)
            {
                var character = _textRange.Characters(i, 1);
                var characterFormattingTags = GetFormattingTags(character);

                // Traverse up the Html tree, until the character can inherit styles and formatting tags
                while (true)
                {
                    // Order of each statement matters.

                    // Line breaks can't have children, traverse up the Html tree.
                    if (currentInnerElement.Tag == Tag.Br)
                        currentInnerElement = currentInnerElement.Parent;

                    // The character is line break and doesn't need to inherit anything.
                    if (character.Text == "\r")
                    {
                        var innerElement = new HtmlElement(currentInnerElement, Tag.Br);
                        currentInnerElement.Children.Add(innerElement);
                        currentInnerElement = innerElement;
                        break;
                    }

                    // The character can inherit all of the currentInnerElement tags and doesn't have additional tags.
                    if (currentInnerElement.InheritingTags == characterFormattingTags)
                    {
                        AddTextToElement(currentInnerElement, character.Text);
                        break;
                    }

                    // The character can inherit all of the currentInnerElement tags and has additional tags.
                    if ((currentInnerElement.InheritingTags | characterFormattingTags) == characterFormattingTags)
                    {
                        var additionalTags = characterFormattingTags ^ currentInnerElement.InheritingTags;
                        currentInnerElement = AddFormattingElements(currentInnerElement, additionalTags);
                        AddTextToElement(currentInnerElement, character.Text);
                        break;
                    }

                    // Reached the parent, the character can inherit because the parent doesn't have any formatting or styling.
                    if (currentInnerElement.Parent == null)
                    {
                        currentInnerElement = AddFormattingElements(currentInnerElement, characterFormattingTags);
                        AddTextToElement(currentInnerElement, character.Text);
                        break;
                    }

                    // Traverse up
                    currentInnerElement = currentInnerElement.Parent;
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