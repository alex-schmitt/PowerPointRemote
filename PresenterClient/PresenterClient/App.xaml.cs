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
        private readonly IPowerPointService _powerPointService;
        private readonly ISignalRService _signalRService;

        public App()
        {
            _powerPointService = new PowerPointService();
            _signalRService = new SignalRService();
            GC.KeepAlive(new ChannelBackgroundService(_powerPointService, _signalRService));

            // The initial start of the signalR service.  Start/stop will later be invoked by UI control only.
            _signalRService.StartAsync().ConfigureAwait(false);
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance(_powerPointService);
            containerRegistry.RegisterInstance(_signalRService);
        }
    }
}