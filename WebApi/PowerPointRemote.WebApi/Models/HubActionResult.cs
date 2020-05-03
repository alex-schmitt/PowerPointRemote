using System.Net;

namespace PowerPointRemote.WebApi.Models
{
    public class HubActionResult
    {
        public HubActionResult(HttpStatusCode status, object body = null)
        {
            Status = status;
            Body = body;
        }

        public HttpStatusCode Status { get; }
        public object Body { get; }
    }
}