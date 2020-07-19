using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using PresenterClient.NoteConverter;
using PresenterClient.SignalR.Messages;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace PresenterClient.Services
{
    public class PowerPointService : IPowerPointService
    {
        private Application _application;

        private string[] _currentSlideShowNotes;

        public PowerPointService()
        {
            LoadAsync().ConfigureAwait(false);
            CurrentSlideDetail = new SlideDetailMsg();
            CurrentSlideShowDetail = new SlideShowDetailMsg();
        }

        public SlideDetailMsg CurrentSlideDetail { get; }

        public SlideShowDetailMsg CurrentSlideShowDetail { get; }

        public SlideShowWindow CurrentSlideShowWindow { get; private set; }

        public event EventHandler<SlideShowDetailMsg> SlideShowChanged;

        public event EventHandler<SlideDetailMsg> SlideChanged;

        public void Dispose()
        {
            Marshal.ReleaseComObject(_application);
        }

        private async Task LoadAsync()
        {
            _application = await Task.Run(() => new Application());
            CurrentSlideShowWindow = _application.SlideShowWindows.Cast<SlideShowWindow>().LastOrDefault();

            if (CurrentSlideShowWindow != null)
            {
                SetCurrentSlidesShowDetail(CurrentSlideShowWindow);
                SetCurrentSlideDetail(CurrentSlideShowWindow);
            }

            _application.SlideShowNextSlide += ApplicationOnSlideShowNextSlide;
            _application.SlideShowEnd += ApplicationOnSlideShowEnd;
        }

        private void SetCurrentSlidesShowDetail(SlideShowWindow slideShowWindow)
        {
            if (slideShowWindow == null)
            {
                CurrentSlideShowDetail.Started = false;
                CurrentSlideShowDetail.SlideCount = 0;
            }
            else
            {
                var presentation = slideShowWindow.Presentation;
                var slides = presentation.Slides;

                _currentSlideShowNotes = new string[slides.Count];
                for (var i = 0; i < slides.Count; i++)
                    _currentSlideShowNotes[i] = GetSlideNotes(slides[i + 1]);

                CurrentSlideShowDetail.SlideCount = slideShowWindow.Presentation.Slides.Count;
                CurrentSlideShowDetail.Started = true;
            }
        }

        private void SetCurrentSlideDetail(SlideShowWindow slideShowWindow)
        {
            var view = slideShowWindow.View;

            CurrentSlideDetail.CurrentPosition = view.CurrentShowPosition;
            CurrentSlideDetail.CurrentSlideNotes = _currentSlideShowNotes[view.CurrentShowPosition - 1];
        }

        private void ApplicationOnSlideShowEnd(Presentation presentation)
        {
            /*
                It's possible that the last active slide show was not the slide show that ended.
                The first clause is true if the active slide show ended manually.
                The second clause is true if the active slide show ended by clicking through to the end.
             */
            if (!_application.SlideShowWindows.Cast<SlideShowWindow>().Contains(CurrentSlideShowWindow) ||
                CurrentSlideShowWindow.View.State == PpSlideShowState.ppSlideShowDone)
            {
                CurrentSlideShowWindow = null;

                CurrentSlideShowDetail.SlideCount = 0;
                CurrentSlideShowDetail.Started = false;
                CurrentSlideDetail.CurrentPosition = 0;
                CurrentSlideDetail.CurrentSlideNotes = null;

                SlideChanged?.Invoke(this, CurrentSlideDetail);
                SlideShowChanged?.Invoke(this, CurrentSlideShowDetail);
            }
        }

        private void ApplicationOnSlideShowNextSlide(SlideShowWindow slideShowWindow)
        {
            var presentation = slideShowWindow.Presentation;
            var slides = presentation.Slides;
            var view = slideShowWindow.View;

            if (CurrentSlideShowWindow != slideShowWindow)
            {
                _currentSlideShowNotes = new string[slides.Count];
                for (var i = 0; i < slides.Count; i++)
                    _currentSlideShowNotes[i] = GetSlideNotes(slides[i + 1]);

                SetCurrentSlidesShowDetail(slideShowWindow);
                SlideShowChanged?.Invoke(this, CurrentSlideShowDetail);
            }

            if (view.CurrentShowPosition != CurrentSlideDetail.CurrentPosition ||
                CurrentSlideShowWindow != slideShowWindow)
            {
                SetCurrentSlideDetail(slideShowWindow);
                SlideChanged?.Invoke(this, CurrentSlideDetail);
            }

            CurrentSlideShowWindow = slideShowWindow;
        }

        private static string GetSlideNotes(Slide slide)
        {
            if (slide == null || slide.HasNotesPage == MsoTriState.msoFalse)
                return null;

            var bodyRange = GetSlideNoteBodyRange(slide);

            if (bodyRange == null)
                return null;

            var noteBuilder = new NoteTreeBuilder();
            var tree = noteBuilder.Build(bodyRange);

            var stringBuilder = new StringBuilder();
            tree.WriteHtml(stringBuilder);
            return stringBuilder.ToString();
        }

        private static TextRange GetSlideNoteBodyRange(Slide slide)
        {
            // There are two placeholders in a notes page, one is the note body and the other is the slide image. The one with text is the note body.
            var shape = slide.NotesPage.Shapes.Placeholders.Cast<Shape>()
                .FirstOrDefault(s => s.TextFrame.HasText == MsoTriState.msoTrue);

            return shape?.TextFrame.TextRange;
        }
    }
}