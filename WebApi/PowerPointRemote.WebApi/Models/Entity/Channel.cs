using System.Collections.Generic;

namespace PowerPointRemote.WebApi.Models.Entity
{
    public class Channel
    {
        public string Id { get; set; }
        public string HostConnectionId { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}