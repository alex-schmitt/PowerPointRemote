using PresenterClient.SignalR;
using Prism.Mvvm;

namespace PresenterClient.ViewModels
{
    public class ConnectionDetailViewModel : BindableBase
    {
        private string _uri;

        public ConnectionDetailViewModel(IChannelService channelService)
        {
            channelService.ChannelStarted += ChannelServiceOnChannelStarted;
        }

        public string Uri
        {
            get => _uri;
            set => SetProperty(ref _uri, value);
        }

        private void ChannelServiceOnChannelStarted(object sender, string channelUri)
        {
            Uri = $"{Constants.WebClientAddress}/{channelUri}";
        }
    }
}