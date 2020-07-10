using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PowerPointRemote.WebAPI.Data;
using PowerPointRemote.WebAPI.Data.Repositories;
using PowerPointRemote.WebApi.Extensions;
using PowerPointRemote.WebApi.Models;

namespace PowerPointRemote.WebApi.Hubs
{
    public class UserHub : Hub
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IHostConnectionRepository _hostConnectionRepository;
        private readonly IHubContext<HostHub> _hostHubContext;

        public UserHub(ApplicationDbContext applicationDbContext, IHubContext<HostHub> hostHubContext,
            IHostConnectionRepository hostConnectionRepository)
        {
            _applicationDbContext = applicationDbContext;
            _hostHubContext = hostHubContext;
            _hostConnectionRepository = hostConnectionRepository;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Guid.Parse(Context.User.Identity.Name ?? string.Empty);
            var channelId = Context.User.FindFirst("ChannelId").Value;

            var user = await _applicationDbContext.Users.SingleOrDefaultAsync(u => u.Id == userId);
            user.Connections++;

            var hostConnectionId = _hostConnectionRepository.GetConnection(channelId);
            if (user.Connections == 1 && hostConnectionId != null)
                await _hostHubContext.SendUserConnected(hostConnectionId, user.Id, user.Name);

            await _applicationDbContext.SaveChangesAsync();
            await Groups.AddToGroupAsync(Context.ConnectionId, channelId);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Guid.Parse(Context.User.Identity.Name ?? string.Empty);
            var channelId = Context.User.FindFirst("ChannelId").Value;

            var user = await _applicationDbContext.Users.SingleOrDefaultAsync(u => u.Id == userId);
            user.Connections--;

            var hostConnectionId = _hostConnectionRepository.GetConnection(channelId);
            if (user.Connections == 0 && hostConnectionId != null)
                await _hostHubContext.SendUserDisconnected(hostConnectionId, userId);

            await _applicationDbContext.SaveChangesAsync();
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, channelId);
        }

        public async Task<HubActionResult> SendSlideShowCommand(byte code)
        {
            var userId = Guid.Parse(Context.User.Identity.Name ?? string.Empty);
            var channelId = Context.User.FindFirst("ChannelId").Value;
            var hostConnectionId = _hostConnectionRepository.GetConnection(channelId);

            if (hostConnectionId == null) return new HubActionResult(HttpStatusCode.NotFound);

            await _hostHubContext.SendSlideShowCommand(hostConnectionId, new SlideShowCommand
            {
                Code = code,
                DateTime = DateTime.Now,
                UserId = userId
            });

            return new HubActionResult(HttpStatusCode.OK);
        }

        public async Task<HubActionResult> GetSlideShowDetail()
        {
            var channelId = Context.User.FindFirst("ChannelId").Value;

            var slideShowDetail =
                await _applicationDbContext.SlideShowDetail.SingleOrDefaultAsync(ssd => ssd.ChannelId == channelId);

            if (slideShowDetail == null) return new HubActionResult(HttpStatusCode.NotFound);

            var slideShowDetailUpdate = new SlideShowDetailUpdate
            {
                SlideShowEnabled = slideShowDetail.Enabled,
                SlideShowName = slideShowDetail.Name,
                CurrentSlide = slideShowDetail.CurrentSlide,
                TotalSlides = slideShowDetail.TotalSlides,
                Timestamp = slideShowDetail.LastUpdate
            };

            return new HubActionResult(HttpStatusCode.OK, slideShowDetailUpdate);
        }
    }
}