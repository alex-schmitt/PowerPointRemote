using System;
using Microsoft.Office.Interop.PowerPoint;

namespace PresenterClient.Services
{
    public interface IPowerPointService : IDisposable
    {
        SlideShowWindow ActiveSlideShowWindow { get; set; }
        event EventHandler<SlideShowWindow> ActiveSlideShowWindowChanged;
    }
}