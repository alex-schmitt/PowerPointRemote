﻿using System;
using System.Collections.Generic;

namespace PowerPointRemote.WebApi.Models.EntityFramework
{
    public class Channel
    {
        public string Id { get; set; }
        public bool ChannelEnded { get; set; }
        public int SlideCount { get; set; }
        public int SlidePosition { get; set; }
        public DateTime LastUpdate { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}