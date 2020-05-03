using System;
using System.Collections.Generic;

namespace PowerPointRemote.WebApi.Models.Entity
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserConnection> Connections { get; set; }

        public string ChannelId { get; set; }
        public virtual Channel Channel { get; set; }
    }
}