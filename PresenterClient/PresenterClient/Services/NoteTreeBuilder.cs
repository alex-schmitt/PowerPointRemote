using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using PresenterClient.NoteConverter;

namespace PresenterClient.Services
{
    // Builds a data structure to assist with converting a slide shows slide into Html.
    public class NoteTreeBuilder
    {
        public RootNode Build(TextRange fullTextRange)
        {
            var rootNode = new RootNode();
            AddParagraphTree(fullTextRange, rootNode);
            return rootNode;
        }

        public ParagraphNode CreateParagraphNode(TextRange paragraph)
        {
            return new ParagraphNode
            {
                Alignment = ConvertAlignment(paragraph.ParagraphFormat.Alignment),
                List = ConvertList(paragraph.ParagraphFormat.Bullet.Type),
                Indent = paragraph.IndentLevel - 1
            };
        }

        public void AddParagraphTree(TextRange fullTextRange, RootNode rootNode)
        {
            for (var i = 1; i <= fullTextRange.Paragraphs().Count; i++)
            {
                var paragraphTextRange = fullTextRange.Paragraphs(i, 1);
                var paragraphNode = CreateParagraphNode(paragraphTextRange);

                if (paragraphTextRange.Text == "\r")
                    paragraphNode.IsLineBreak = true;

                rootNode.Children.Add(paragraphNode);
                AddStringTree(paragraphTextRange, paragraphNode);
            }
        }

        public StringNode CreateStringNode(TextRange character, StringNode parent)
        {
            return new StringNode(parent)
            {
                InlineTags = GetInlineFlags(character),
                FontColor = character.Font.Color.RGB,
                FontSize = character.Font.Size
            };
        }

        public void AddStringTree(TextRange paragraph, ParagraphNode paragraphNode)
        {
            StringNode currentNode = null;

            for (var x = 1; x <= paragraph.Characters().Count; x++)
            {
                var character = paragraph.Characters(x, 1);
                var characterFlags = GetInlineFlags(character);
                while (true)
                {
                    if (currentNode != null && characterFlags == currentNode.InlineTags)
                    {
                        currentNode.InnerText.Append(character.Text);
                        break;
                    }

                    if (currentNode != null && (characterFlags | currentNode.InlineTags) == characterFlags)
                    {
                        var node = CreateStringNode(character, currentNode);
                        node.InnerText.Append(character.Text);
                        currentNode.Children.Add(node);
                        currentNode = node;
                        break;
                    }

                    if (currentNode?.Parent == null)
                    {
                        var node = CreateStringNode(character, null);
                        node.InnerText.Append(character.Text);
                        paragraphNode.Children.Add(node);
                        currentNode = node;
                        break;
                    }

                    currentNode = currentNode.Parent;
                }
            }
        }

        public static InlineTag GetInlineFlags(TextRange character)
        {
            var flag = InlineTag.None;
            if (character.Font.Bold == MsoTriState.msoTrue)
                flag |= InlineTag.Bold;
            if (character.Font.Italic == MsoTriState.msoTrue)
                flag |= InlineTag.Italic;
            if (character.Font.Underline == MsoTriState.msoTrue)
                flag |= InlineTag.Underline;
            if (character.Font.Subscript == MsoTriState.msoTrue)
                flag |= InlineTag.Subscript;
            if (character.Font.Superscript == MsoTriState.msoTrue)
                flag |= InlineTag.Superscript;

            return flag;
        }

        public static Alignment ConvertAlignment(PpParagraphAlignment ppAlignment)
        {
            switch (ppAlignment)
            {
                case PpParagraphAlignment.ppAlignLeft:
                    return Alignment.Left;
                case PpParagraphAlignment.ppAlignCenter:
                    return Alignment.Center;
                case PpParagraphAlignment.ppAlignRight:
                    return Alignment.Right;
                case PpParagraphAlignment.ppAlignJustify:
                    return Alignment.Justify;
                default:
                    return Alignment.Left;
            }
        }

        public static List ConvertList(PpBulletType ppBullet)
        {
            if (ppBullet == PpBulletType.ppBulletUnnumbered)
                return List.Bullet;
            if (ppBullet == PpBulletType.ppBulletNumbered)
                return List.Ordered;

            return List.None;
        }
    }
}