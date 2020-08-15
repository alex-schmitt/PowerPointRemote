using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using PowerPointRemote.WebAPI.Data;
using PowerPointRemote.WebAPI.Data.Repositories;
using PowerPointRemote.WebApi.Extensions;
using PowerPointRemote.WebApi.Models;
using PowerPointRemote.WebApi.Models.EntityFramework;
using PowerPointRemote.WebApi.Models.Messages;

namespace PowerPointRemote.WebApi.Hubs
{
    public class HostHub : Hub
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IHostConnectionRepository _hostConnectionRepository;
        private readonly IHubContext<UserHub> _userHubContext;
        private readonly IUserPermissionRepository _userPermissionRepository;

        public HostHub(ApplicationDbContext applicationDbContext, IHubContext<UserHub> userHubContext,
            IHostConnectionRepository hostConnectionRepository, IUserPermissionRepository userPermissionRepository)
        {
            _applicationDbContext = applicationDbContext;
            _userHubContext = userHubContext;
            _hostConnectionRepository = hostConnectionRepository;
            _userPermissionRepository = userPermissionRepository;
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

        public Task SetUserPermission(UserPermissionMsg userPermissionMsg)
        {
            _userPermissionRepository.SetPermission(userPermissionMsg.UserId,
                new UserPermission {AllowControl = userPermissionMsg.AllowControl});
            return Task.CompletedTask;
        }

        public async Task SetSlideCount(int count)
        {
            var channelId = Context.User.FindFirst("ChannelId").Value;

            await _userHubContext.SendSlideCount(channelId, count);

            var channelUpdate = new Channel {Id = channelId, SlideCount = count};
            _applicationDbContext.Attach(channelUpdate);
            _applicationDbContext.Entry(channelUpdate).Property(p => p.SlideCount).IsModified = true;

            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task SetSlidePosition(int position)
        {
            var channelId = Context.User.FindFirst("ChannelId").Value;

            await _userHubContext.SendSetSlidePosition(channelId, position);

            var channelUpdate = new Channel {Id = channelId, SlidePosition = position};
            _applicationDbContext.Attach(channelUpdate);
            _applicationDbContext.Entry(channelUpdate).Property(p => p.SlidePosition).IsModified = true;

            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task StopChannel()
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