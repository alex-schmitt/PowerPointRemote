using PresenterClient.Services;
using Prism.Commands;
using Prism.Mvvm;

namespace PresenterClient.ViewModels
{
    public class ChannelControlsViewModel : BindableBase
    {
        private readonly ISignalRService _signalRService;
        private bool _isNewChannelEnabled;

        public ChannelControlsViewModel(ISignalRService signalRService)
        {
            _signalRService = signalRService;
            NewChannelCommand = new DelegateCommand(NewChannel).ObservesProperty(() => IsNewChannelEnabled);
        }

        public bool IsNewChannelEnabled
        {
            get => _isNewChannelEnabled;
            set => SetProperty(ref _isNewChannelEnabled, value);
        }

        public DelegateCommand NewChannelCommand { get; }

        private async void NewChannel()
        {
            IsNewChannelEnabled = false;
            await _signalRService.StopAsync();
            await _signalRService.StartAsync();
            IsNewChannelEnabled = true;
        }
    }
}