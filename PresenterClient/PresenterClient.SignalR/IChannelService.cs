using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace PresenterClient.SignalR
{
    public interface IChannelService : IDisposable
    {
        HubConnectionState State { get; }
        event EventHandler<string> ChannelStarted;
        event EventHandler<SlideShowCommand> SlideShowCommandReceived;
        event EventHandler<User> UserConnected;
        Task StartChannel();
        Task EndChannel();
        Task UpdateSlideShowDetail(SlideShowDetail slideShowDetail);
    }
}