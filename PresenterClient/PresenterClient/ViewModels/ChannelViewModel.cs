using System;
using PresenterClient.Common;
using PresenterClient.Services;
using Prism.Mvvm;

namespace PresenterClient.ViewModels
{
    public class ChannelViewModel : BindableBase
    {
        private string _uri;

        public ChannelViewModel(ISignalRService signalRService)
        {
            signalRService.Started += SignalRServiceOnStarted;
        }

        private static string WebClientAddress => Util.IsDebug ? "http://localhost:3000" : "https://ppremote.com";

        public string Uri
        {
            get => _uri;
            set => SetProperty(ref _uri, value);
        }

        private void SignalRServiceOnStarted(object sender, EventArgs e)
        {
            var service = (SignalRService) sender;
            Uri = $"{WebClientAddress}/{service.ChannelId}";
        }
    }
}