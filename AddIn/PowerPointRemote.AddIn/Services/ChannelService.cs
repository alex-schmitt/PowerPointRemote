using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using PowerPointRemote.AddIn.Extensions;

namespace PowerPointRemote.AddIn.Services
{
    public class ChannelService : IChannelService
    {
        private readonly HttpClient _httpClient;
        private HubConnection _hubConnection;

        public ChannelService()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.BaseAddress = new Uri("https://localhost:5001");
        }

        public event EventHandler<SlideShowCommand> SlideShowCommandReceived;

        public event EventHandler<User> UserConnected;
        public event EventHandler<Guid> UserDisconnected;
        public event EventHandler<string> ConnectionStatusChanged;

        public event EventHandler ChannelStartRequest;
        public event EventHandler<Channel> ChannelStartSuccess;
        public event EventHandler<string> ChannelStartFailure;

        public event EventHandler ChannelEndRequest;
        public event EventHandler ChannelEndSuccess;


        public async Task StartChannel()
        {
            ChannelStartRequest?.Invoke(this, EventArgs.Empty);

            try
            {
                var response = await PostCreateChannel();
                var channel = await response.Content.Deserialize<Channel>();
                await StartHubConnection(channel.AccessToken);

                ChannelStartSuccess?.Invoke(this, channel);
            }
            catch (Exception)
            {
                ChannelStartFailure?.Invoke(this, "Connection Error");
            }
        }

        public async Task StopChannel()
        {
            ChannelEndRequest?.Invoke(this, EventArgs.Empty);

            await StopHubConnection();

            ChannelEndSuccess?.Invoke(this, EventArgs.Empty);
        }

        public IEnumerable<User> GetChannelUsers()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
            _hubConnection?.DisposeAsync();
        }

        private async Task<HttpResponseMessage> PostCreateChannel()
        {
            var responseMessage = await _httpClient.PostAsync("create-channel", null);
            return responseMessage;
        }

        private async Task StartHubConnection(string accessToken)
        {
            await Task.Run(() =>
            {
                _hubConnection = _hubConnection = new HubConnectionBuilder()
                    .WithUrl(Constants.HostHubAddress,
                        options => { options.AccessTokenProvider = () => Task.FromResult(accessToken); })
                    .WithAutomaticReconnect()
                    .Build();

                StartMethodListeners();
                StartConnectionListeners();
            });


            await _hubConnection.StartAsync();
        }

        private async Task StopHubConnection()
        {
            if (_hubConnection != null)
            {
                await _hubConnection.DisposeAsync();
                _hubConnection = null;
            }
        }

        private void StartMethodListeners()
        {
            _hubConnection.On<User>("UserConnected", OnUserConnected);
            _hubConnection.On<User>("UserDisconnected", OnUserDisconnected);
            _hubConnection.On<SlideShowCommand>("SlideShowCommand", OnSlideShowCommandReceived);
        }

        private void StartConnectionListeners()
        {
            _hubConnection.Closed += HubConnectionOnClosed;
            _hubConnection.Reconnecting += HubConnectionOnReconnecting;
            _hubConnection.Reconnected += HubConnectionOnReconnected;
        }

        private void OnUserDisconnected(User user)
        {
            UserDisconnected?.Invoke(this, user.Id);
        }

        private void OnUserConnected(User user)
        {
            UserConnected?.Invoke(this, user);
        }

        private void OnSlideShowCommandReceived(SlideShowCommand slideShowCommand)
        {
            SlideShowCommandReceived?.Invoke(this, slideShowCommand);
        }

        private Task HubConnectionOnReconnected(string arg)
        {
            ConnectionStatusChanged?.Invoke(this, "Connected");
            return Task.CompletedTask;
        }

        private Task HubConnectionOnReconnecting(Exception arg)
        {
            ConnectionStatusChanged?.Invoke(this, "Reconnecting");
            return Task.CompletedTask;
        }

        private Task HubConnectionOnClosed(Exception arg)
        {
            ConnectionStatusChanged?.Invoke(this, "Disconnected");
            return Task.CompletedTask;
        }
    }
}