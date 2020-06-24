using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Office.Interop.PowerPoint;

namespace PowerPointRemote.DesktopClient.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly ChannelService _channelService;
        private readonly SlideShowManager _slideShowManager;

        public MainWindowViewModel(ChannelService channelService, SlideShowManager slideShowManager)
        {
            _channelService = channelService;
            _slideShowManager = slideShowManager;

            ConnectedUsers = new ObservableCollection<User>();
        }

        public ObservableCollection<User> ConnectedUsers { get; }


        public string ChannelUri => _channelService.ChannelId == null
            ? null
            : $"{Constants.WebClientAddress}/{_channelService.ChannelId}";

        public async Task LoadAsync()
        {
            await _channelService.StartChannel();
            OnPropertyChanged(nameof(ChannelUri));

            _channelService.HubConnection.On<User>("UserConnected", AddUser);
            _channelService.HubConnection.On<User>("UserDisconnected", RemoveUser);
            _channelService.HubConnection.On<SlideShowCommand>("SlideShowCommand", _slideShowManager.ProcessCommand);

            _slideShowManager.Application.SlideShowNextSlide += async ssw => await SendSlideShowMeta(ssw);
            _slideShowManager.Application.SlideShowEnd += async pres => await SendSlideShowMeta(pres);

            if (_slideShowManager.LastOpenedSlideShow != null)
                await SendSlideShowMeta(_slideShowManager.LastOpenedSlideShow);
        }

        private async Task SendSlideShowMeta(SlideShowWindow slideShowWindow)
        {
            await SendSlideShowMeta(slideShowWindow.Presentation, slideShowWindow.View);
        }

        private async Task SendSlideShowMeta(Presentation presentation, SlideShowView slideShowView = null)
        {
            await _channelService.SendSlideShowMeta(new SlideShowMeta
            {
                SlideShowEnabled = _slideShowManager.LastOpenedSlideShow != null,
                Title = presentation?.Name ?? "",
                CurrentSlide = slideShowView?.CurrentShowPosition ?? 0,
                TotalSlides = presentation?.Slides.Count ?? 0,
                Timestamp = DateTime.Now
            });
        }

        private void AddUser(User user)
        {
            ConnectedUsers.Add(user);
        }

        private void RemoveUser(User user)
        {
            var connectedUser = ConnectedUsers.FirstOrDefault(u => u.Id == user.Id);
            ConnectedUsers.Remove(connectedUser);
        }
    }
}