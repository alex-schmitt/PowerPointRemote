using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Office.Interop.PowerPoint;

namespace PresenterClient.Services
{
    public class PresentationService : IPresentationService
    {
        private SlideShowWindow _activeSlideShowWindow;
        private Application _application;
        private int _slideCount;
        private int _slidePosition;

        public PresentationService()
        {
            LoadAsync().ConfigureAwait(false);
        }

        public void NextSlide()
        {
            _activeSlideShowWindow?.View.Next();
        }

        public void PreviousSlide()
        {
            _activeSlideShowWindow?.View.Previous();
        }

        public int SlidePosition
        {
            get => _slidePosition;
            private set
            {
                if (value == _slidePosition) return;
                _slidePosition = value;
                SlidePositionChanged?.Invoke(this, value);
            }
        }

        public int SlideCount
        {
            get => _slideCount;
            private set
            {
                if (value == _slideCount) return;
                _slideCount = value;
                SlideCountChanged?.Invoke(this, value);
            }
        }

        public bool SlideShowActive => _activeSlideShowWindow != null;

        public event EventHandler<int> SlidePositionChanged;
        public event EventHandler<int> SlideCountChanged;

        public void Dispose()
        {
            Marshal.ReleaseComObject(_application);
        }

        private async Task LoadAsync()
        {
            _application = await Task.Run(() => new Application());
            _activeSlideShowWindow = _application.SlideShowWindows.Cast<SlideShowWindow>().LastOrDefault();

            if (_activeSlideShowWindow != null)
                SetSlideShowDetail();

            _application.SlideShowNextSlide += ApplicationOnSlideShowNextSlide;
            _application.SlideShowEnd += ApplicationOnSlideShowEnd;
        }

        private void ApplicationOnSlideShowEnd(Presentation presentation)
        {
            /*
                It's possible that the active slide show was not the slide show that ended.
                The first clause is true if the active slide show ended manually.
                The second clause is true if the active slide show ended by clicking through to the end.
             */
            if (!_application.SlideShowWindows.Cast<SlideShowWindow>().Contains(_activeSlideShowWindow) ||
                _activeSlideShowWindow.View.State == PpSlideShowState.ppSlideShowDone)
            {
                _activeSlideShowWindow = null;
                SlideCount = 0;
                SlidePosition = 0;
            }
        }

        private void SetSlideShowDetail()
        {
            SlidePosition = _activeSlideShowWindow.View.CurrentShowPosition;
            SlideCount = _activeSlideShowWindow.Presentation.Slides.Count;
        }

        private void ApplicationOnSlideShowNextSlide(SlideShowWindow slideShowWindow)
        {
            _activeSlideShowWindow = slideShowWindow;
            SetSlideShowDetail();
        }
    }
}