using PresenterClient.Common;

namespace PresenterClient.SignalR
{
    public static class Constants
    {
        public static string HostHubAddress =>
            Util.IsDebug ? "https://localhost:5001/hub/host" : "https://api.ppremote.com/hub/host";

        public static string ApiAddress => Util.IsDebug ? "https://localhost:5001" : "https://api.ppremote.com";
    }
}