using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace PowerPointRemote.DesktopClient.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly ChannelService _channelService;
        private readonly SlideShowManager _slideShowManager;

        public MainWindowViewModel(ChannelService channelService, SlideShowManager slideShowManager)
        {
            _channelService = channelService;
            _slideShowManager = slideShowManager;

            ConnectedUsers = new ObservableCollection<User>();
        }

        public ObservableCollection<User> ConnectedUsers { get; }


        public string ChannelUri => _channelService.ChannelId == null
            ? null
            : $"{Constants.WebClientAddress}/{_channelService.ChannelId}";

        public async Task LoadAsync()
        {
            await _channelService.StartChannel();
            OnPropertyChanged(nameof(ChannelUri));

            _channelService.HubConnection.On<User>("UserConnected", AddUser);
            _channelService.HubConnection.On<User>("UserDisconnected", RemoveUser);
            _channelService.HubConnection.On<SlideShowCommand>("SlideShowCommand", ProcessSlideShowCommand);
        }

        private void ProcessSlideShowCommand(SlideShowCommand slideShowCommand)
        {
            _slideShowManager.ProcessCommand(slideShowCommand);
        }

        private void AddUser(User user)
        {
            ConnectedUsers.Add(user);
        }

        private void RemoveUser(User user)
        {
            var connectedUser = ConnectedUsers.FirstOrDefault(u => u.Id == user.Id);
            ConnectedUsers.Remove(connectedUser);
        }
    }
}