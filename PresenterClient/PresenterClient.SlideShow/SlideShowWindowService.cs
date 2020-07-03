using System;
using System.Linq;
using Microsoft.Office.Interop.PowerPoint;

namespace PresenterClient.SlideShow
{
    public class SlideShowWindowService : ISlideShowWindowService
    {
        private readonly Application _application;

        public SlideShowWindow SlideShowWindow;

        /// <summary>
        ///     Tracks the last active SlideShowWindow for a given PowerPoint application.
        /// </summary>
        /// <param name="application"></param>
        public SlideShowWindowService(Application application)
        {
            _application = application;

            SlideShowWindow = application.SlideShowWindows.Cast<SlideShowWindow>().LastOrDefault();

            application.SlideShowNextSlide += ApplicationOnSlideShowNextSlide;
            application.SlideShowEnd += ApplicationOnSlideShowEnd;
        }

        public event EventHandler<SlideShowWindow> OnSlideShowWindowChange;

        private void ApplicationOnSlideShowEnd(Presentation presentation)
        {
            /*
                It's possible that the last active slide show was not the slide show that ended.
                The first clause is true if the active slide show ended manually.
                The second clause is true if the active slide show ended by clicking through to the end.
             */
            if (!_application.SlideShowWindows.Cast<SlideShowWindow>().Contains(SlideShowWindow) ||
                SlideShowWindow.View.State == PpSlideShowState.ppSlideShowDone)
                SlideShowWindow = null;
        }

        private void ApplicationOnSlideShowNextSlide(SlideShowWindow slideShowWindow)
        {
            if (SlideShowWindow != slideShowWindow)
                OnSlideShowWindowChange?.Invoke(this, slideShowWindow);
        }
    }
}