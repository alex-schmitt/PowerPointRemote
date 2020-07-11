using System;

namespace PresenterClient.SignalR.Messages
{
    public class UserPermissionMsg
    {
        public Guid UserId { get; set; }
        public bool AllowControl { get; set; }
    }
}