using System;
using Microsoft.Office.Interop.PowerPoint;
using PresenterClient.SignalR.Messages;

namespace PresenterClient.Services
{
    public interface IPowerPointService : IDisposable
    {
        SlideDetailMsg CurrentSlideDetail { get; }
        SlideShowDetailMsg CurrentSlideShowDetail { get; }
        SlideShowWindow CurrentSlideShowWindow { get; }
        event EventHandler<SlideShowDetailMsg> SlideShowChanged;
        event EventHandler<SlideDetailMsg> SlideChanged;
    }
}