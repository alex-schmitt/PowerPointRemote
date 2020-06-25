using System.Collections.ObjectModel;
using System.Linq;

namespace PowerPointRemote.DesktopClient.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private string _channelUri;

        public MainWindowViewModel(ChannelService channelService)
        {
            var channelService1 = channelService;

            channelService1.UserConnected += (sender, user) => AddUser(user);
            channelService1.UserDisconnected += (sender, user) => RemoveUser(user);
            channelService1.ChannelStarted += (sender, channelUri) => ChannelUri = channelUri;

            ConnectedUsers = new ObservableCollection<User>();
        }

        public ObservableCollection<User> ConnectedUsers { get; }


        public string ChannelUri
        {
            get => _channelUri;
            set
            {
                _channelUri = $"{Constants.WebClientAddress}/{value}";
                OnPropertyChanged();
            }
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