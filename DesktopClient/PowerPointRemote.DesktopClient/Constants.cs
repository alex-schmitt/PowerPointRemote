namespace PowerPointRemote.DesktopClient
{
    public static class Constants
    {
        private static bool IsDebug
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
        }

        public static string WebClientAddress => IsDebug ? "http://localhost:3000" : "https://ppremote.com";

        public static string HostHubAddress =>
            IsDebug ? "https://localhost:5001/hub/host" : "https://api.ppremote.com/hub/host";

        public static string ApiAddress => IsDebug ? "https://localhost:5001" : "https://api.ppremote.com";
    }
}