namespace PowerPointRemote.AddIn.Services
{
    public interface ISlideShowControlService
    {
        void InvokeSlideShowCommand(SlideShowCommand slideShowCommand);
    }
}