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
        public bool IsStarted { get; private set; }
        public string ChannelId { get; private set; }
        public HubConnection HubConnection { get; private set; }

        public event EventHandler Started;

        public event EventHandler Stopped;

        public event EventHandler<string> StartFailure;

        public async Task StartAsync()
        {
            if (HubConnection != null)
                throw new InvalidOperationException("The previous channel must be stopped first.");

            try
            {
                var channel = await ApiClient.CreateChannel();
                ChannelId = channel.ChannelId;

                HubConnection = await HubConnectionBuilder.BuildAsync(channel.AccessToken);
                await HubConnection.StartAsync();

                IsStarted = true;
                Started?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception)
            {
                StartFailure?.Invoke(this, "Unable to connected");
            }
        }

        public async Task StopAsync()
        {
            if (!IsStarted)
                return;

            try
            {
                await HubConnection.SendChannelEndedAsync();
            }
            catch (Exception)
            {
                // ignored
            }

            await HubConnection.DisposeAsync();
            HubConnection = null;

            IsStarted = false;
            Stopped?.Invoke(this, EventArgs.Empty);
        }

        public async ValueTask DisposeAsync()
        {
            if (!IsStarted)
                return;

            await StopAsync();
        }
    }
}