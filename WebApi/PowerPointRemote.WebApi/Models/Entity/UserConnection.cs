using System;

namespace PowerPointRemote.WebApi.Models.Entity
{
    public class UserConnection
    {
        public string Id { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public string ChannelId { get; set; }
        public virtual Channel Channel { get; set; }
    }
}