using System;
using Microsoft.AspNetCore.SignalR.Client;
using PresenterClient.SignalR;
using PresenterClient.SignalR.Messages;

namespace PresenterClient.Services
{
    public class ChannelBackgroundService
    {
        private readonly IPowerPointService _powerPointService;
        private readonly ISignalRService _signalRService;

        public ChannelBackgroundService(IPowerPointService powerPointService, ISignalRService signalRService)
        {
            _powerPointService = powerPointService;
            _signalRService = signalRService;

            powerPointService.SlideChanged += OnSlideChanged;
            powerPointService.SlideShowChanged += OnSlideShowChanged;

            signalRService.Started += OnConnectionStarted;
        }

        private void ProcessSlideShowAction(SlideShowActionMsg message)
        {
            var window = _powerPointService.CurrentSlideShowWindow;

            if (window == null)
                return;

            switch (message.Code)
            {
                case 0:
                    window.View.Next();
                    break;
                case 1:
                    window.View.Previous();
                    break;
            }
        }

        private async void OnConnectionStarted(object sender, EventArgs e)
        {
            _signalRService.HubConnection.OnSlideShowActionReceived(ProcessSlideShowAction);
            await _signalRService.HubConnection.SendSetSlideShowDetailAsync(_powerPointService.CurrentSlideShowDetail);
            await _signalRService.HubConnection.SendSetCurrentSlideDetailAsync(_powerPointService.CurrentSlideDetail);
        }

        private async void OnSlideShowChanged(object sender, SlideShowDetailMsg e)
        {
            if (_signalRService.HubConnection.State != HubConnectionState.Connected)
                return;

            await _signalRService.HubConnection.SendSetSlideShowDetailAsync(_powerPointService.CurrentSlideShowDetail);
        }

        private async void OnSlideChanged(object sender, SlideDetailMsg e)
        {
            if (_signalRService.HubConnection.State != HubConnectionState.Connected)
                return;

            await _signalRService.HubConnection.SendSetCurrentSlideDetailAsync(_powerPointService.CurrentSlideDetail);
        }
    }
}