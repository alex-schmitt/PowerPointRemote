using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using PresenterClient.Model;
using PresenterClient.Services;
using PresenterClient.SignalR;
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

            ChannelUsers.CollectionChanged += ChannelUsersOnCollectionChanged;
        }

        public ObservableCollection<ChannelUser> ChannelUsers { get; }

        private void ChannelUsersOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
        {
            if (eventArgs.NewItems != null)
                foreach (var item in eventArgs.NewItems)
                {
                    var user = (ChannelUser) item;
                    user.PropertyChanged += UserOnPropertyChanged;
                }

            if (eventArgs.OldItems != null)
                foreach (var item in eventArgs.OldItems)
                {
                    var user = (ChannelUser) item;
                    user.PropertyChanged -= UserOnPropertyChanged;
                }
        }

        private async void UserOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var user = (ChannelUser) sender;
            await _signalRService.HubConnection.SendUserUpdateAsync(new SignalR.Messages.ChannelUser
            {
                Id = user.Id,
                AllowControl = user.AllowControl
            });
        }

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

        private void RemoveChannelUser(SignalR.Messages.ChannelUser user)
        {
            var connectedUser = ChannelUsers.FirstOrDefault(u => u.Id == user.Id);
            Current.Dispatcher.Invoke(() => ChannelUsers.Remove(connectedUser));
        }

        private void AddChannelUser(SignalR.Messages.ChannelUser user)
        {
            Current.Dispatcher.Invoke(() => ChannelUsers.Add(new ChannelUser
            {
                Name = user.Name,
                AllowControl = user.AllowControl,
                Id = user.Id
            }));
        }
    }
}