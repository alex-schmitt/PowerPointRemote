using System.Net;

namespace PowerPointRemote.WebApi.Models
{
    public class HubActionResult
    {
        public HubActionResult(HttpStatusCode status, object data = null)
        {
            Status = status;
            Data = data;
        }

        public HttpStatusCode Status { get; }
        public object Data { get; }
    }
}