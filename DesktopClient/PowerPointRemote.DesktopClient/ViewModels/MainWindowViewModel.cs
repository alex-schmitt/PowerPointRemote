using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using PowerPointRemote.DesktopClient.Commands;

namespace PowerPointRemote.DesktopClient.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private string _channelUri;

        public MainWindowViewModel(ChannelService channelService)
        {
            channelService.UserConnected += (sender, user) => AddUser(user);
            channelService.UserDisconnected += (sender, user) => RemoveUser(user);
            channelService.ChannelStarted += (sender, channelUri) => ChannelUri = channelUri;

            ConnectedUsers = new ObservableCollection<User>();
            NewChannelCommand = new NewChannelCommand(channelService);
            CopyTextBoxCommand = new CopyTextCommand();
        }

        public ObservableCollection<User> ConnectedUsers { get; }

        public ICommand NewChannelCommand { get; }

        public ICommand CopyTextBoxCommand { get; }


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