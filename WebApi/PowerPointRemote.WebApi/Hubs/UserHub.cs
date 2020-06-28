using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PowerPointRemote.WebAPI.Data;
using PowerPointRemote.WebApi.Extensions;
using PowerPointRemote.WebApi.Models;
using PowerPointRemote.WebApi.Models.Entity;

namespace PowerPointRemote.WebApi.Hubs
{
    public class UserHub : Hub
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IHubContext<HostHub> _hostHubContext;
        private readonly IMemoryCache _memoryCache;

        public UserHub(ApplicationDbContext applicationDbContext, IMemoryCache memoryCache,
            IHubContext<HostHub> hostHubContext)
        {
            _applicationDbContext = applicationDbContext;
            _memoryCache = memoryCache;
            _hostHubContext = hostHubContext;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Guid.Parse(Context.User.Identity.Name);
            var channelId = Context.User.FindFirst("ChannelId").Value;
            var hostConnectionId = await GetHostConnectionIdAsync(channelId);

            var user = await _applicationDbContext.Users.Include(u => u.Connections)
                .SingleOrDefaultAsync(u => u.Id == userId);

            if (user.Connections == null)
                user.Connections = new List<UserConnection>();

            user.Connections.Add(new UserConnection
            {
                Id = Context.ConnectionId,
                ChannelId = user.ChannelId
            });

            await _applicationDbContext.SaveChangesAsync();

            if (user.Connections.Count == 1 && hostConnectionId != null)
                await _hostHubContext.SendUserConnected(hostConnectionId, user.Id, user.Name);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Guid.Parse(Context.User.Identity.Name);
            var channelId = Context.User.FindFirst("ChannelId").Value;
            var hostConnectionId = await GetHostConnectionIdAsync(channelId);

            var usersConnectionCount =
                await _applicationDbContext.UserConnections.Where(uc => uc.UserId == userId).CountAsync();

            if (usersConnectionCount == 1 && hostConnectionId != null)
                await _hostHubContext.SendUserDisconnected(hostConnectionId, userId);

            _applicationDbContext.UserConnections.Remove(new UserConnection {Id = Context.ConnectionId});
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<HubActionResult> SendSlideShowCommand(byte code)
        {
            var userId = Guid.Parse(Context.User.Identity.Name);
            var channelId = Context.User.FindFirst("ChannelId").Value;
            var hostConnectionId = await GetHostConnectionIdAsync(channelId);

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

        private async Task<string> GetHostConnectionIdAsync(string channelId)
        {
            return await _memoryCache.GetOrCreateAsync(channelId, async entry =>
            {
                var channel = await _applicationDbContext.Channels.FindAsync(channelId);
                return channel.HostConnectionId;
            });
        }
    }
}