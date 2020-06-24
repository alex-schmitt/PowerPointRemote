using System;
using System.Collections.Generic;

namespace PowerPointRemote.WebApi.Models.Entity
{
    public class Channel
    {
        public string Id { get; set; }
        public string HostConnectionId { get; set; }
        public bool SlideShowEnabled { get; set; }

        public string SlideShowTitle { get; set; }

        public int CurrentSlide { get; set; }

        public int TotalSlides { get; set; }

        public DateTime LastUpdate { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}