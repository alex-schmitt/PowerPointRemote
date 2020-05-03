using System;
using System.Linq;
using System.Windows.Forms;
using PowerPointRemote.AddIn.Services;
using PowerPointRemote.AddIn.Views;

namespace PowerPointRemote.AddIn.Presenters
{
    public class ChannelViewPresenter
    {
        private readonly IChannelService _channelService;
        private readonly IChannelView _view;

        public ChannelViewPresenter(IChannelView view, IChannelService channelService)
        {
            _view = view;
            _view.Presenter = this;
            _view.ClientAddress = Constants.WebClientAddress;

            _channelService = channelService;
            channelService.ChannelStartSuccess += OnChannelStartSuccess;
            channelService.UserConnected += ChannelModelOnUserConnected;
            channelService.UserDisconnected += ChannelModelOnUserDisconnected;
            channelService.ConnectionStatusChanged += ChannelServiceOnConnectionStatusChanged;

            CreateNewChannel();
        }

        private void ChannelServiceOnConnectionStatusChanged(object sender, string statusMessage)
        {
            _view.InvokeSafe(() => _view.StatusMessage = statusMessage);
        }

        public async void CreateNewChannel()
        {
            await _channelService.StartChannel();
        }

        public async void EndChannel()
        {
            await _channelService.StopChannel();
        }

        public void CopyUriToClipboard()
        {
            Clipboard.SetText(_view.ChannelUri);
        }

        private void ChannelModelOnUserDisconnected(object sender, Guid userId)
        {
            var user = _view.UserConnections.SingleOrDefault(u => u.Id == userId);
            if (user != null)
                _view.InvokeSafe(() => { _view.RemoveChannelUser(user); });
        }

        private void ChannelModelOnUserConnected(object sender, User user)
        {
            _view.InvokeSafe(() => { _view.AddChannelUser(user); });
        }

        private void OnChannelStartSuccess(object sender, Channel channel)
        {
            _view.InvokeSafe(() =>
            {
                _view.ChannelId = channel.ChannelId;
                _view.ChannelUri = $"{Constants.WebClientAddress}/{channel.ChannelId}";
                _view.StatusMessage = "Connected";
                _view.ClearChannelUserList();
            });
        }
    }
}