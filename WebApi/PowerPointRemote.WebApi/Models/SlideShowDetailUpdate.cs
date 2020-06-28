using System;

namespace PowerPointRemote.WebApi.Models
{
    public class SlideShowDetailUpdate
    {
        public bool SlideShowEnabled { get; set; }
        public string SlideShowName { get; set; }
        public int CurrentSlide { get; set; }
        public int TotalSlides { get; set; }
        public DateTime Timestamp { get; set; }
    }
}