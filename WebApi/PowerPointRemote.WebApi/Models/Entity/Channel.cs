using System;
using System.Collections.Generic;

namespace PowerPointRemote.WebApi.Models.Entity
{
    public class Channel
    {
        public string Id { get; set; }
        public bool ChannelEnded { get; set; }
        public DateTime LastUpdate { get; set; }

        public virtual SlideShowDetail SlideShowDetail { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}