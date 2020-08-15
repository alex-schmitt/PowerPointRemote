using System;

namespace PresenterClient.Services
{
    public interface IPresentationService : IDisposable
    {
        int SlidePosition { get; }
        int SlideCount { get; }
        bool SlideShowActive { get; }
        void NextSlide();
        void PreviousSlide();
        event EventHandler<int> SlidePositionChanged;
        event EventHandler<int> SlideCountChanged;
    }
}