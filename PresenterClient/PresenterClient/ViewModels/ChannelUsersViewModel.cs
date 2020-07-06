using System;
using System.Collections.ObjectModel;
using System.Linq;
using PresenterClient.Services;
using PresenterClient.SignalR;
using PresenterClient.SignalR.Messages;
using Prism.Mvvm;
using static System.Windows.Application;

namespace PresenterClient.ViewModels
{
    public class ChannelUsersViewModel : BindableBase
    {
        private readonly ISignalRService _signalRService;

        public ChannelUsersViewModel(ISignalRService signalRService)
        {
            _signalRService = signalRService;

            signalRService.Started += SignalRServiceOnStarted;
            signalRService.Stopped += SignalRServiceOnStopped;

            ChannelUsers = new ObservableCollection<ChannelUser>();
        }

        public ObservableCollection<ChannelUser> ChannelUsers { get; }

        private void SignalRServiceOnStarted(object sender, EventArgs e)
        {
            var service = (SignalRService) sender;

            service.HubConnection.OnUserConnected(AddChannelUser);
            service.HubConnection.OnUserDisconnected(RemoveChannelUser);
        }

        private void SignalRServiceOnStopped(object sender, EventArgs e)
        {
            Current.Dispatcher.Invoke(() => ChannelUsers.Clear());
        }

        private void RemoveChannelUser(ChannelUser user)
        {
            var connectedUser = ChannelUsers.FirstOrDefault(u => u.Id == user.Id);
            Current.Dispatcher.Invoke(() => ChannelUsers.Remove(connectedUser));
        }

        private void AddChannelUser(ChannelUser user)
        {
            Current.Dispatcher.Invoke(() => ChannelUsers.Add(user));
        }
    }
}