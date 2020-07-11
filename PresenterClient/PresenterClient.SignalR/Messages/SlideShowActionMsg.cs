using System;

namespace PresenterClient.SignalR.Messages
{
    public class SlideShowActionMsg
    {
        public byte Code { get; set; }
        public DateTime DateTime { get; set; }
        public Guid UserId { get; set; }
    }
}