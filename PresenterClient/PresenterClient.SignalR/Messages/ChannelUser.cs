using System;

namespace PresenterClient.SignalR.Messages
{
    public class ChannelUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool AllowControl { get; set; }
    }
}