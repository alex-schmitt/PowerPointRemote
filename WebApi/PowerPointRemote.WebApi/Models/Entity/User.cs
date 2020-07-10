using System;

namespace PowerPointRemote.WebApi.Models.Entity
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Connections { get; set; }

        public string ChannelId { get; set; }
        public virtual Channel Channel { get; set; }
    }
}