using System.Linq;
using Microsoft.Office.Interop.PowerPoint;

namespace PowerPointRemote.DesktopClient
{
    public class SlideShowManager
    {
        public readonly Application Application;

        public SlideShowManager(Application application)
        {
            Application = application;
        }

        public SlideShowWindow LastOpenedSlideShow =>
            Application.SlideShowWindows.Cast<SlideShowWindow>()
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