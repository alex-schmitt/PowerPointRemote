using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using PowerPointRemote.DesktopClient.Extensions;

namespace PowerPointRemote.DesktopClient
{
    public class ChannelService : IDisposable
    {
        private readonly HttpClient _httpClient;

        public ChannelService()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.BaseAddress = new Uri(Constants.ApiAddress);
        }

        public string ChannelId { get; private set; }

        public HubConnection HubConnection { get; private set; }

        public void Dispose()
        {
            _httpClient?.Dispose();
            HubConnection?.DisposeAsync();
        }

        public async Task StartChannel()
        {
            var response = await CreateChannel();
            var channel = await response.Content.Deserialize<Channel>();
            await StartHubConnection(channel.AccessToken);
            ChannelId = channel.ChannelId;
        }

        public async Task StopChannel()
        {
            await StopHubConnection();
            ChannelId = null;
        }

        public async Task SendSlideShowMeta(SlideShowMeta slideShowMeta)
        {
            await HubConnection.SendAsync("SendSlideShowMeta", slideShowMeta);
        }

        private async Task<HttpResponseMessage> CreateChannel()
        {
            var responseMessage = await _httpClient.PostAsync("create-channel", null);
            return responseMessage;
        }

        private async Task StartHubConnection(string accessToken)
        {
            await Task.Run(() =>
            {
                HubConnection = HubConnection = new HubConnectionBuilder()
                    .WithUrl(Constants.HostHubAddress,
                        options => { options.AccessTokenProvider = () => Task.FromResult(accessToken); })
                    .WithAutomaticReconnect()
                    .Build();
            });

            await HubConnection.StartAsync();
        }

        private async Task StopHubConnection()
        {
            if (HubConnection != null)
            {
                await HubConnection.DisposeAsync();
                HubConnection = null;
            }
        }
    }
}