using System;

namespace PowerPointRemote.WebApi.Models.EntityFramework
{
    public class SlideShowDetail
    {
        public Guid Id { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public int CurrentSlide { get; set; }
        public int TotalSlides { get; set; }
        public DateTime LastUpdate { get; set; }

        public string ChannelId { get; set; }
        public virtual Channel Channel { get; set; }
    }
}