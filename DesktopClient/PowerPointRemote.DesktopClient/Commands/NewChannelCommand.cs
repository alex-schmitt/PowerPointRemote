using System;
using System.Windows.Input;
using Microsoft.AspNetCore.SignalR.Client;

namespace PowerPointRemote.DesktopClient.Commands
{
    public class NewChannelCommand : ICommand
    {
        private readonly ChannelService _channelService;

        private bool _isProcessing;

        public NewChannelCommand(ChannelService channelService)
        {
            _channelService = channelService;
        }

        public bool CanExecute(object parameter)
        {
            return !_isProcessing;
        }

        public async void Execute(object parameter)
        {
            _isProcessing = true;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);

            if (_channelService.State != HubConnectionState.Disconnected)
                await _channelService.EndChannel();

            await _channelService.StartChannel();

            _isProcessing = false;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged;
    }
}