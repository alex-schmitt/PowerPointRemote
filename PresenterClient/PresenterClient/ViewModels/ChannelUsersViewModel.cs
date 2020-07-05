using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.AspNetCore.SignalR.Client;
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
        private Visibility _waitingVisibility;

        public ChannelUsersViewModel(ISignalRService signalRService)
        {
            _signalRService = signalRService;

            signalRService.Started += SignalRServiceOnStarted;
            signalRService.Stopped += SignalRServiceOnStopped;

            ChannelUsers = new ObservableCollection<ChannelUser>();

            UpdateWaitingVisibility();
        }

        public ObservableCollection<ChannelUser> ChannelUsers { get; }

        public Visibility WaitingVisibility
        {
            get => _waitingVisibility;
            set => SetProperty(ref _waitingVisibility, value);
        }

        private void SignalRServiceOnStarted(object sender, EventArgs e)
        {
            UpdateWaitingVisibility();
            var service = (SignalRService) sender;

            service.HubConnection.OnUserConnected(AddChannelUser);
            service.HubConnection.OnUserDisconnected(RemoveChannelUser);
        }

        private void SignalRServiceOnStopped(object sender, EventArgs e)
        {
            UpdateWaitingVisibility();
            Current.Dispatcher.Invoke(() => ChannelUsers.Clear());
        }

        private void RemoveChannelUser(ChannelUser user)
        {
            UpdateWaitingVisibility();
            var connectedUser = ChannelUsers.FirstOrDefault(u => u.Id == user.Id);
            Current.Dispatcher.Invoke(() => ChannelUsers.Remove(connectedUser));
        }

        private void AddChannelUser(ChannelUser user)
        {
            UpdateWaitingVisibility();
            Current.Dispatcher.Invoke(() => ChannelUsers.Add(user));
        }

        private void UpdateWaitingVisibility()
        {
            if (_signalRService.HubConnection?.State == HubConnectionState.Connected && ChannelUsers.Count < 1 &&
                WaitingVisibility != Visibility.Visible)
                WaitingVisibility = Visibility.Visible;
            else if (_waitingVisibility != Visibility.Collapsed || _waitingVisibility != Visibility.Hidden)
                WaitingVisibility = Visibility.Collapsed;
        }
    }
}