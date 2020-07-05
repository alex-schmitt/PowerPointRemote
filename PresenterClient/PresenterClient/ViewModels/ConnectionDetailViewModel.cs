using System;
using System.Windows;
using PresenterClient.Common;
using PresenterClient.Services;
using Prism.Commands;
using Prism.Mvvm;

namespace PresenterClient.ViewModels
{
    public class ConnectionDetailViewModel : BindableBase
    {
        private string _uri;

        public ConnectionDetailViewModel(ISignalRService signalRService)
        {
            signalRService.Started += SignalRServiceOnStarted;
            signalRService.Stopped += SignalRServiceOnStopped;

            CopyUriCommand = new DelegateCommand(CopyUri);
        }

        private static string WebClientAddress => Util.IsDebug ? "http://localhost:3000" : "https://ppremote.com";

        public string Uri
        {
            get => _uri;
            set => SetProperty(ref _uri, value);
        }

        public DelegateCommand CopyUriCommand { get; }

        private void CopyUri()
        {
            Clipboard.SetText(_uri ?? "");
        }

        private void SignalRServiceOnStopped(object sender, EventArgs e)
        {
            Uri = "";
        }

        private void SignalRServiceOnStarted(object sender, EventArgs e)
        {
            var service = (SignalRService) sender;
            Uri = $"{WebClientAddress}/{service.ChannelId}";
        }
    }
}