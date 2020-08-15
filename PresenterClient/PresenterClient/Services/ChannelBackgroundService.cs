using System;
using PresenterClient.SignalR;
using PresenterClient.SignalR.Messages;

namespace PresenterClient.Services
{
    public class ChannelBackgroundService
    {
        private readonly IPresentationService _presentationService;
        private readonly ISignalRService _signalRService;

        public ChannelBackgroundService(IPresentationService presentationService, ISignalRService signalRService)
        {
            _presentationService = presentationService;
            _signalRService = signalRService;

            presentationService.SlideCountChanged += OnSlideCountChanged;
            presentationService.SlidePositionChanged += OnSlidePositionChanged;

            signalRService.Started += OnConnectionStarted;
        }

        private void OnSlidePositionChanged(object sender, int e)
        {
            _signalRService.HubConnection.SendSetSlideShowPosition(e);
        }

        private void OnSlideCountChanged(object sender, int e)
        {
            _signalRService.HubConnection.SendSetSlideShowCount(e);
        }

        private void ProcessSlideShowAction(SlideShowActionMsg message)
        {
            switch (message.Code)
            {
                case 0:
                    _presentationService.NextSlide();
                    break;
                case 1:
                    _presentationService.PreviousSlide();
                    break;
            }
        }

        private async void OnConnectionStarted(object sender, EventArgs e)
        {
            var hubConnection = _signalRService.HubConnection;
            hubConnection.OnSlideShowActionReceived(ProcessSlideShowAction);

            await hubConnection.SendSetSlideShowCount(_presentationService.SlideCount);
            await hubConnection.SendSetSlideShowPosition(_presentationService.SlidePosition);
        }
    }
}