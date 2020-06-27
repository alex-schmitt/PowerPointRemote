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

        private HubConnection _hubConnection;

        public EventHandler<string> ChannelStarted;

        public EventHandler<SlideShowCommand> SlideShowCommandReceived;

        public EventHandler<User> UserConnected;

        public EventHandler<User> UserDisconnected;

        public ChannelService()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.BaseAddress = new Uri(Constants.ApiAddress);
        }

        public HubConnectionState State => _hubConnection?.State ?? HubConnectionState.Disconnected;

        public void Dispose()
        {
            if (_hubConnection != null)
                throw new InvalidOperationException("End the channel before disposing.");

            _httpClient.Dispose();
        }

        public async Task StartChannel()
        {
            var response = await CreateChannel();
            var channel = await response.Content.Deserialize<Channel>();
            var hubConnection = await StartHubConnection(channel.AccessToken);

            hubConnection.On<User>("UserConnected", user => UserConnected?.Invoke(this, user));
            hubConnection.On<User>("UserDisconnected", user => UserDisconnected?.Invoke(this, user));
            hubConnection.On<SlideShowCommand>("SlideShowCommand",
                command => SlideShowCommandReceived?.Invoke(this, command));

            ChannelStarted?.Invoke(this, channel.ChannelId);
        }

        public async Task EndChannel()
        {
            if (_hubConnection == null)
                return;

            await _hubConnection.SendAsync("EndChannel");
            await _hubConnection.DisposeAsync();
            _hubConnection = null;
        }

        public async Task UpdateSlideShowDetail(SlideShowDetail slideShowDetail)
        {
            await _hubConnection.SendAsync("UpdateSlideShowDetail", slideShowDetail);
        }

        private async Task<HttpResponseMessage> CreateChannel()
        {
            var responseMessage = await _httpClient.PostAsync("create-channel", null);
            return responseMessage;
        }

        private async Task<HubConnection> StartHubConnection(string accessToken)
        {
            await Task.Run(() =>
            {
                _hubConnection = _hubConnection = new HubConnectionBuilder()
                    .WithUrl(Constants.HostHubAddress,
                        options => { options.AccessTokenProvider = () => Task.FromResult(accessToken); })
                    .WithAutomaticReconnect()
                    .Build();
            });

            await _hubConnection.StartAsync();
            return _hubConnection;
        }
    }
}