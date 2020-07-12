using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using PresenterClient.NoteConverter;

namespace PresenterClient.Services
{
    public class PowerPointService : IPowerPointService
    {
        private SlideShowWindow _activeSlideShowWindow;
        private Application _application;

        public PowerPointService()
        {
            LoadAsync().ConfigureAwait(false);
        }

        public SlideShowWindow ActiveSlideShowWindow
        {
            get => _activeSlideShowWindow;
            set
            {
                _activeSlideShowWindow = value;
                ActiveSlideShowWindowChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<SlideShowWindow> ActiveSlideShowWindowChanged;

        public void Dispose()
        {
            Marshal.ReleaseComObject(_application);
        }

        private async Task LoadAsync()
        {
            _application = await Task.Run(() => new Application());
            ActiveSlideShowWindow = _application.SlideShowWindows.Cast<SlideShowWindow>().LastOrDefault();

            _application.SlideShowNextSlide += ApplicationOnSlideShowNextSlide;
            _application.SlideShowEnd += ApplicationOnSlideShowEnd;
        }

        private void ApplicationOnSlideShowEnd(Presentation presentation)
        {
            /*
                It's possible that the last active slide show was not the slide show that ended.
                The first clause is true if the active slide show ended manually.
                The second clause is true if the active slide show ended by clicking through to the end.
             */
            if (!_application.SlideShowWindows.Cast<SlideShowWindow>().Contains(ActiveSlideShowWindow) ||
                ActiveSlideShowWindow.View.State == PpSlideShowState.ppSlideShowDone)
                ActiveSlideShowWindow = null;
        }

        private void ApplicationOnSlideShowNextSlide(SlideShowWindow slideShowWindow)
        {
            ActiveSlideShowWindow = slideShowWindow;
        }

        public static string BuildNotes(SlideShowWindow slideShowWindow)
        {
            var currentSlidePosition = slideShowWindow.View.CurrentShowPosition;
            var slide = slideShowWindow.Presentation.Slides[currentSlidePosition];

            if (slide?.HasNotesPage != MsoTriState.msoTrue)
                return "";

            var delta = new Delta();
            var frameTextRange = slide.NotesPage.Shapes.Placeholders[2].TextFrame.TextRange;
            var paragraphCount = frameTextRange.Paragraphs().Count;

            for (var i = 1; i <= paragraphCount; i++)
            {
                var paragraph = frameTextRange.Paragraphs(i, 1);
                var characterCount = paragraph.Characters().Count;

                for (var x = 1; x <= characterCount; x++)
                {
                    var character = paragraph.Characters(x, 1);

                    if (character.Text == "\r")
                        break;

                    AddCharOperation(delta, character);
                }

                AddLineOperation(delta, paragraph);
            }

            return JsonSerializer.Serialize(delta.Ops);
        }

        private static void AddCharOperation(Delta delta, TextRange character)
        {
            var charFont = character.Font;

            // nullable bool used here because the serialized delta is required to be compact and the Json serializer will ignore null values.
            bool? bold = true;
            bool? italic = true;
            bool? underline = true;

            if (charFont.Bold != MsoTriState.msoTrue)
                bold = null;
            if (charFont.Italic != MsoTriState.msoTrue)
                italic = null;
            if (charFont.Underline != MsoTriState.msoTrue)
                underline = null;

            var previousOp = delta.PreviousOp;
            var prevAttributes = previousOp?.Attributes;
            var containsAttributes = charFont.Bold == MsoTriState.msoTrue ||
                                     charFont.Italic == MsoTriState.msoTrue ||
                                     charFont.Underline == MsoTriState.msoTrue;
            var matchesPreviousAttributes = !containsAttributes && prevAttributes == null ||
                                            previousOp?.Attributes != null &&
                                            prevAttributes.Bold == bold &&
                                            prevAttributes.Italic == italic &&
                                            prevAttributes.Underline == underline;

            if (previousOp != null && matchesPreviousAttributes && previousOp.Insert != "\n")
                previousOp.Insert += character.Text;
            else if (!containsAttributes)
                delta.AddOp(new DeltaOp {Insert = character.Text});
            else
                delta.AddOp(new DeltaOp
                {
                    Insert = character.Text,
                    Attributes = new DeltaAttributes
                    {
                        Bold = bold,
                        Italic = italic,
                        Underline = underline
                    }
                });
        }

        private static void AddLineOperation(Delta delta, TextRange paragraph)
        {
            var bulletType = paragraph.ParagraphFormat.Bullet.Type;

            List? list = null;
            if (bulletType == PpBulletType.ppBulletNumbered)
                list = List.Ordered;
            else if (bulletType != PpBulletType.ppBulletNone)
                list = List.Bullet;

            var previousOp = delta.PreviousOp;
            var prevAttributes = previousOp?.Attributes;
            var containsAttributes = bulletType != PpBulletType.ppBulletNone;

            if (!containsAttributes)
                delta.AddOp(new DeltaOp {Insert = "\n"});
            else if (previousOp?.Insert == "\n" && list != null && prevAttributes?.List == list)
                /*
                     * PowerPoint notes ~can~ have lines breaks between sequential list items, but Html cannot.
                     * By adding this attribute, margins can instead be added to the Html list items.
                     */
                prevAttributes.BottomSpacing =
                    prevAttributes.BottomSpacing == null ? 1 : prevAttributes.BottomSpacing + 1;
            else
                delta.AddOp(new DeltaOp
                {
                    Insert = "\n",
                    Attributes = new DeltaAttributes
                    {
                        List = list
                    }
                });
        }
    }
}