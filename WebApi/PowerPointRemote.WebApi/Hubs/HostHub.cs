using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PowerPointRemote.WebAPI.Data;
using PowerPointRemote.WebAPI.Data.Repositories;
using PowerPointRemote.WebApi.Extensions;
using PowerPointRemote.WebApi.Models;

namespace PowerPointRemote.WebApi.Hubs
{
    public class HostHub : Hub
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IHostConnectionRepository _hostConnectionRepository;
        private readonly IHubContext<UserHub> _userHubContext;

        public HostHub(ApplicationDbContext applicationDbContext, IHubContext<UserHub> userHubContext,
            IHostConnectionRepository hostConnectionRepository)
        {
            _applicationDbContext = applicationDbContext;
            _userHubContext = userHubContext;
            _hostConnectionRepository = hostConnectionRepository;
        }

        public override async Task OnConnectedAsync()
        {
            var channelId = Context.User.FindFirst("ChannelId").Value;
            _hostConnectionRepository.SetConnection(channelId, Context.ConnectionId);
            await _userHubContext.SendHostConnected(channelId);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var channelId = Context.User.FindFirst("ChannelId").Value;
            _hostConnectionRepository.RemoveConnection(channelId);
            await _userHubContext.SendHostDisconnected(channelId);
        }

        public async Task UpdateSlideShowDetail(SlideShowDetailUpdate slideShowDetailUpdate)
        {
            var channelId = Context.User.FindFirst("ChannelId").Value;

            await _userHubContext.SendSlideShowDetail(channelId, slideShowDetailUpdate);

            var slideShowDetail =
                await _applicationDbContext.SlideShowDetail.SingleOrDefaultAsync(ssd => ssd.ChannelId == channelId);

            slideShowDetail.Enabled = slideShowDetailUpdate.SlideShowEnabled;
            slideShowDetail.Name = slideShowDetailUpdate.SlideShowName;
            slideShowDetail.CurrentSlide = slideShowDetailUpdate.CurrentSlide;
            slideShowDetail.TotalSlides = slideShowDetailUpdate.TotalSlides;
            slideShowDetail.LastUpdate = slideShowDetailUpdate.Timestamp;

            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task EndChannel()
        {
            var channelId = Context.User.FindFirst("ChannelId").Value;

            var channel = await _applicationDbContext.Channels.FindAsync(channelId);
            channel.ChannelEnded = true;
            channel.LastUpdate = DateTime.Now;

            await _applicationDbContext.SaveChangesAsync();

            await _userHubContext.SendChannelEnded(channelId);
        }
    }
}