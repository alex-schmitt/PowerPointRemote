using System;
using System.Windows;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Office.Interop.PowerPoint;
using PresenterClient.Services;
using PresenterClient.SignalR;
using PresenterClient.SignalR.Messages;
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

            // The initial start of the signalR service.  Start/stop will later be invoked by UI control only.
            _signalRService.StartAsync().ConfigureAwait(false);

            ConnectServices();
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

        private void ConnectServices()
        {
            _signalRService.Started += SignalRServiceOnStarted;
        }

        private void ProcessSlideShowAction(SlideShowAction slideShowAction)
        {
            var wn = _powerPointService.ActiveSlideShowWindow;

            if (wn == null)
                return;

            switch (slideShowAction.Code)
            {
                case 0:
                    wn.View.Next();
                    break;
                case 1:
                    wn.View.Previous();
                    break;
            }
        }

        private async void OnActiveSlideShowWindowChanged(object sender, SlideShowWindow e)
        {
            if (_signalRService.HubConnection?.State != HubConnectionState.Connected)
                return;

            await _signalRService.HubConnection.SendSlideShowDetailAsync(
                CreateSlideShowDetail(_powerPointService.ActiveSlideShowWindow));
        }

        private async void SignalRServiceOnStarted(object sender, EventArgs e)
        {
            _powerPointService.ActiveSlideShowWindowChanged += OnActiveSlideShowWindowChanged;
            _signalRService.HubConnection.OnSlideShowActionReceived(ProcessSlideShowAction);

            // Send the initial SlideShowDetail if available
            if (_powerPointService.ActiveSlideShowWindow != null)
                await _signalRService.HubConnection.SendSlideShowDetailAsync(
                    CreateSlideShowDetail(_powerPointService.ActiveSlideShowWindow));
        }

        private static SlideShowDetail CreateSlideShowDetail(SlideShowWindow slideShowWindow)
        {
            return new SlideShowDetail
            {
                Name = slideShowWindow?.Presentation.Name ?? "",
                SlideShowEnabled = slideShowWindow != null,
                CurrentSlide = slideShowWindow?.View.CurrentShowPosition ?? 0,
                TotalSlides = slideShowWindow?.Presentation.Slides.Count ?? 0,
                Timestamp = DateTime.Now
            };
        }
    }
}