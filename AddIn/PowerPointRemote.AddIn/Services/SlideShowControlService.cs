using Microsoft.Office.Interop.PowerPoint;

namespace PowerPointRemote.AddIn.Services
{
    public class SlideShowControlService : ISlideShowControlService
    {
        private readonly SlideShowWindows _slideShowWindows;

        public SlideShowControlService(SlideShowWindows slideShowWindows)
        {
            _slideShowWindows = slideShowWindows;
        }

        public void InvokeSlideShowCommand(SlideShowCommand slideShowCommand)
        {
            if (_slideShowWindows.Count == 0)
                return;

            foreach (SlideShowWindow slideShowWindow in _slideShowWindows)
            {
                var view = slideShowWindow.View;

                switch (slideShowCommand.Code)
                {
                    case 0:
                        view.Next();
                        break;
                    case 1:
                        view.Previous();
                        break;
                    // no default case
                }
            }
        }
    }
}