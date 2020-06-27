using System;
using System.Collections.Generic;
using System.Linq;
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
    public class HostHub : Hub
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMemoryCache _memoryCache;
        private readonly IHubContext<UserHub> _userHubContext;

        public HostHub(ApplicationDbContext applicationDbContext, IMemoryCache memoryCache,
            IHubContext<UserHub> userHubContext)
        {
            _applicationDbContext = applicationDbContext;
            _memoryCache = memoryCache;
            _userHubContext = userHubContext;
        }

        public override async Task OnConnectedAsync()
        {
            var channelId = Context.User.FindFirst("ChannelId").Value;
            var channel = await _applicationDbContext.Channels.FindAsync(channelId);
            channel.HostConnectionId = Context.ConnectionId;
            await _applicationDbContext.SaveChangesAsync();

            _memoryCache.Set(channelId, Context.ConnectionId);

            await _userHubContext.SendHostConnected(await GetUserConnections(channelId));
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var channelId = Context.User.FindFirst("ChannelId").Value;
            var channel = await _applicationDbContext.Channels.FindAsync(channelId);

            if (channel == null)
                return;

            channel.HostConnectionId = null;
            await _applicationDbContext.SaveChangesAsync();

            _memoryCache.Remove(channelId);

            await _userHubContext.SendHostDisconnected(await GetUserConnections(channelId));
        }

        public async Task UpdateSlideShowDetail(SlideShowDetail slideShowDetail)
        {
            var channelId = Context.User.FindFirst("ChannelId").Value;
            await _userHubContext.SendSlideShowDetail(await GetUserConnections(channelId), slideShowDetail);

            var channel = await _applicationDbContext.Channels.FindAsync(channelId);
            channel.SlideShowEnabled = slideShowDetail.SlideShowEnabled;
            channel.SlideShowName = slideShowDetail.SlideShowName;
            channel.CurrentSlide = slideShowDetail.CurrentSlide;
            channel.TotalSlides = slideShowDetail.TotalSlides;
            channel.LastUpdate = slideShowDetail.Timestamp;

            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task EndChannel()
        {
            var channelId = Context.User.FindFirst("ChannelId").Value;

            var channel = await _applicationDbContext.Channels.FindAsync(channelId);
            channel.ChannelEnded = true;
            channel.LastUpdate = DateTime.Now;

            await _applicationDbContext.SaveChangesAsync();

            await _userHubContext.SendChannelEnded(await GetUserConnections(channelId));
        }

        private Task<List<UserConnection>> GetUserConnections(string channelId)
        {
            return _applicationDbContext.UserConnections.AsNoTracking()
                .Where(conn => conn.ChannelId == channelId).ToListAsync();
        }
    }
}