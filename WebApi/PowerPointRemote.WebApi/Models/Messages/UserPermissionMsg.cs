using System;

namespace PowerPointRemote.WebApi.Models.Messages
{
    public class UserPermissionMsg
    {
        public Guid UserId { get; set; }
        public bool AllowControl { get; set; }
    }
}