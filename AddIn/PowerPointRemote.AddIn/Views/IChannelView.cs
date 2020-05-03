using System;
using System.Collections.Generic;
using PowerPointRemote.AddIn.Presenters;
using PowerPointRemote.AddIn.Services;

namespace PowerPointRemote.AddIn.Views
{
    public interface IChannelView : IDisposable
    {
        ChannelViewPresenter Presenter { set; }
        string ChannelId { set; }
        string ChannelUri { get; set; }
        string StatusMessage { set; }
        string ClientAddress { set; }
        IEnumerable<User> UserConnections { get; }
        void AddChannelUser(User user);
        void RemoveChannelUser(User userId);
        void ClearChannelUserList();
        void InvokeSafe(Action action);
    }
}