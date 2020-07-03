using System;

namespace PresenterClient.SignalR.Messages
{
    public class RemoteUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}