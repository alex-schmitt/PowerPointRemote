using System;

namespace PowerPointRemote.WebApi.Models.Messages
{
    public class SlideShowActionMsg
    {
        public int Code { get; set; }
        public DateTime DateTime { get; set; }
        public Guid UserId { get; set; }
    }
}