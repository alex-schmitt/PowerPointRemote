using System;

namespace PowerPointRemote.WebApi.Models
{
    public class SlideShowMeta
    {
        public bool SlideShowEnabled { get; set; }
        public string Title { get; set; }
        public int CurrentSlide { get; set; }
        public int TotalSlides { get; set; }
        public DateTime Timestamp { get; set; }
    }
}