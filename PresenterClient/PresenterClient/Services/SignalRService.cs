using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using PresenterClient.Http;
using PresenterClient.SignalR;
using HubConnectionBuilder = PresenterClient.SignalR.HubConnectionBuilder;

namespace PresenterClient.Services
{
    public class SignalRService : ISignalRService
    {
        public bool IsDisposed { get; private set; }
        public string ChannelId { get; private set; }
        public HubConnection HubConnection { get; private set; }

        public event EventHandler Started;

        public event EventHandler Stopped;

        public async Task StartAsync()
        {
            if (HubConnection != null)
                throw new InvalidOperationException("The previous channel must be stopped first.");

            var channel = await ApiClient.CreateChannel();
            ChannelId = channel.ChannelId;

            HubConnection = await HubConnectionBuilder.BuildAsync(channel.AccessToken);
            await HubConnection.StartAsync();

            Started?.Invoke(this, EventArgs.Empty);
        }

        public async Task StopAsync()
        {
            if (HubConnection != null)
            {
                await HubConnection.SendChannelEndedAsync();
                await HubConnection.StopAsync();
            }

            Stopped?.Invoke(this, EventArgs.Empty);
        }

        public async ValueTask DisposeAsync()
        {
            if (HubConnection == null)
                return;

            await StopAsync();
            await HubConnection.DisposeAsync();
            IsDisposed = true;
        }
    }
}