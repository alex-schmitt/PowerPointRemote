using System;
using System.Windows;
using PresenterClient.Services;
using PresenterClient.Views;
using Prism.Ioc;

namespace PresenterClient
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private readonly IPresentationService _presentationService;
        private readonly ISignalRService _signalRService;

        public App()
        {
            _presentationService = new PresentationService();
            _signalRService = new SignalRService();
            GC.KeepAlive(new ChannelBackgroundService(_presentationService, _signalRService));

            // The initial start of the signalR service.  Start/stop will later be invoked by UI control only.
            _signalRService.StartAsync().ConfigureAwait(false);
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance(_presentationService);
            containerRegistry.RegisterInstance(_signalRService);
        }
    }
}