using System;

namespace PowerPointRemote.WebApi.Models
{
    public class SlideShowCommand
    {
        public int Code { get; set; }
        public DateTime DateTime { get; set; }
        public Guid UserId { get; set; }
    }
}