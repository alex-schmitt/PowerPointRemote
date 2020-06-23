using System.Linq;
using Microsoft.Office.Interop.PowerPoint;

namespace PowerPointRemote.DesktopClient
{
    public class SlideShowManager
    {
        private readonly Application _application;

        public SlideShowManager(Application application)
        {
            _application = application;
        }

        public SlideShowWindow LastOpenedSlideShow =>
            _application.SlideShowWindows.Cast<SlideShowWindow>()
                .LastOrDefault();

        public void ProcessCommand(SlideShowCommand command)
        {
            switch (command.Code)
            {
                case 0:
                    NextSlide();
                    break;
                case 1:
                    PreviousSlide();
                    break;
                // no default case
            }
        }

        private void NextSlide()
        {
            LastOpenedSlideShow?.View.Next();
        }

        private void PreviousSlide()
        {
            LastOpenedSlideShow?.View.Previous();
        }
    }
}