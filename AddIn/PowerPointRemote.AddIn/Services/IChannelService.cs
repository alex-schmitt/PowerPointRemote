using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PowerPointRemote.AddIn.Services
{
    public interface IChannelService : IDisposable
    {
        Task StartChannel();
        Task StopChannel();
        IEnumerable<User> GetChannelUsers();

        event EventHandler<SlideShowCommand> SlideShowCommandReceived;
        event EventHandler<string> ConnectionStatusChanged;

        event EventHandler<User> UserConnected;
        event EventHandler<Guid> UserDisconnected;

        event EventHandler ChannelStartRequest;
        event EventHandler<Channel> ChannelStartSuccess;
        event EventHandler<string> ChannelStartFailure;

        event EventHandler ChannelEndRequest;
        event EventHandler ChannelEndSuccess;
    }
}