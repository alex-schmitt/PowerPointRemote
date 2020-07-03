using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace PresenterClient.Services
{
    public interface ISignalRService : IAsyncDisposable
    {
        bool IsDisposed { get; }
        string ChannelId { get; }
        HubConnection HubConnection { get; }
        event EventHandler Started;
        event EventHandler Stopped;
        Task StartAsync();
        Task StopAsync();
    }
}