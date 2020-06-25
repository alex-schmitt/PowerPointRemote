using System;
using System.Linq;
using Microsoft.Office.Interop.PowerPoint;

namespace PowerPointRemote.DesktopClient
{
    public class SlideShowManager
    {
        private readonly Application _application;

        private SlideShowWindow _slideShowWindow;

        public SlideShowManager(Application application)
        {
            _application = application;
            SlideShowDetail = new SlideShowDetail();

            _slideShowWindow = application.SlideShowWindows.Cast<SlideShowWindow>().LastOrDefault();
            if (_slideShowWindow != null)
                SetSlideShowDetail(_slideShowWindow);

            application.SlideShowNextSlide += ApplicationOnSlideShowNextSlide;
            application.SlideShowEnd += ApplicationOnSlideShowEnd;
        }

        public SlideShowDetail SlideShowDetail { get; }

        public event EventHandler<SlideShowDetail> OnSlideShowDetailChange;

        public void ProcessCommand(SlideShowCommand command)
        {
            switch (command.Code)
            {
                case 0:
                    NextClick();
                    break;
                case 1:
                    PreviousClick();
                    break;
                // no default case
            }
        }

        private void NextClick()
        {
            _slideShowWindow?.View.Next();
        }

        private void PreviousClick()
        {
            _slideShowWindow?.View.Previous();
        }

        private void ApplicationOnSlideShowEnd(Presentation presentation)
        {
            if (_application.SlideShowWindows.Cast<SlideShowWindow>().Contains(_slideShowWindow))
                return;

            _slideShowWindow = null;
            ResetSlideShowDetail();
        }

        private void ApplicationOnSlideShowNextSlide(SlideShowWindow slideShowWindow)
        {
            _slideShowWindow = slideShowWindow;
            SetSlideShowDetail(slideShowWindow);
        }

        private void ResetSlideShowDetail()
        {
            SlideShowDetail.SlideShowEnabled = false;
            SlideShowDetail.Title = "";
            SlideShowDetail.CurrentSlide = 0;
            SlideShowDetail.TotalSlides = 0;
            SlideShowDetail.Timestamp = DateTime.Now;

            OnSlideShowDetailChange?.Invoke(this, SlideShowDetail);
        }

        private void SetSlideShowDetail(SlideShowWindow slideShowWindow)
        {
            SlideShowDetail.SlideShowEnabled = true;
            SlideShowDetail.Title = slideShowWindow.Presentation.Name;
            SlideShowDetail.CurrentSlide = slideShowWindow.View.CurrentShowPosition;
            SlideShowDetail.TotalSlides = slideShowWindow.Presentation.Slides.Count;
            SlideShowDetail.Timestamp = DateTime.Now;

            OnSlideShowDetailChange?.Invoke(this, SlideShowDetail);
        }
    }
}