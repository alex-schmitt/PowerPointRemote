using System;
using Microsoft.Office.Interop.PowerPoint;

namespace PresenterClient.SlideShow
{
    public interface ISlideShowWindowService
    {
        event EventHandler<SlideShowWindow> OnSlideShowWindowChange;
    }
}