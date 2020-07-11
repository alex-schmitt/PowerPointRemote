using System;

namespace PowerPointRemote.WebApi.Models.Messages
{
    public class ChannelUserMsg
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool AllowControl { get; set; }
    }
}