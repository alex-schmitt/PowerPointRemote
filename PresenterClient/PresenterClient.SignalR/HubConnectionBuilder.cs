using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using PresenterClient.Common;

namespace PresenterClient.SignalR
{
    public static class HubConnectionBuilder
    {
        private static readonly string SignalRHubAddress =
            Util.IsDebug ? "https://localhost:5001/hub/host" : "https://api.ppremote.com/hub/host";

        public static async Task<HubConnection> BuildAsync(string accessToken)
        {
            return await Task.Run(() =>
            {
                return new Microsoft.AspNetCore.SignalR.Client.HubConnectionBuilder()
                    .WithUrl(SignalRHubAddress,
                        options => { options.AccessTokenProvider = () => Task.FromResult(accessToken); })
                    .WithAutomaticReconnect()
                    .Build();
            });
        }
    }
}