using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Office.Interop.PowerPoint;

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
    }
}