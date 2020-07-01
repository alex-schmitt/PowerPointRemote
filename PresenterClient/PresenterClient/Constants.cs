using PresenterClient.Common;

namespace PresenterClient
{
    public static class Constants
    {
        public static string WebClientAddress => Util.IsDebug ? "http://localhost:3000" : "https://ppremote.com";
    }
}