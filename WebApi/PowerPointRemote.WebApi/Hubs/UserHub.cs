using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PowerPointRemote.WebAPI.Data;
using PowerPointRemote.WebAPI.Data.Repositories;
using PowerPointRemote.WebApi.Extensions;
using PowerPointRemote.WebApi.Models;
using PowerPointRemote.WebApi.Models.Messages;

namespace PowerPointRemote.WebApi.Hubs
{
    [Authorize]
    public class UserHub : Hub
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IHostConnectionRepository _hostConnectionRepository;
        private readonly IHubContext<HostHub> _hostHubContext;
        private readonly IUserPermissionRepository _userPermissionRepository;

        public UserHub(ApplicationDbContext applicationDbContext, IHubContext<HostHub> hostHubContext,
            IHostConnectionRepository hostConnectionRepository, IUserPermissionRepository userPermissionRepository)
        {
            _applicationDbContext = applicationDbContext;
            _hostHubContext = hostHubContext;
            _hostConnectionRepository = hostConnectionRepository;
            _userPermissionRepository = userPermissionRepository;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Guid.Parse(Context.User.Identity.Name ?? string.Empty);
            var channelId = Context.User.FindFirst("ChannelId").Value;

            var user = await _applicationDbContext.Users.SingleOrDefaultAsync(u => u.Id == userId);
            user.Connections++;

            var permission = _userPermissionRepository.GetPermission(userId);
            if (permission == null)
            {
                permission = new UserPermission {AllowControl = true};
                _userPermissionRepository.SetPermission(userId, permission);
            }

            var hostConnectionId = _hostConnectionRepository.GetConnection(channelId);
            if (user.Connections == 1 && hostConnectionId != null)
                await _hostHubContext.SendUserConnected(hostConnectionId,
                    new ChannelUserMsg {Id = user.Id, Name = user.Name, AllowControl = permission.AllowControl});

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
            {
                await _hostHubContext.SendUserDisconnected(hostConnectionId, userId);
                _userPermissionRepository.RemoveUser(userId);
            }

            await _applicationDbContext.SaveChangesAsync();
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, channelId);
        }

        public async Task<HubActionResult> SendSlideShowCommand(byte code)
        {
            var userId = Guid.Parse(Context.User.Identity.Name ?? string.Empty);

            if (!_userPermissionRepository.GetPermission(userId).AllowControl)
                return new HubActionResult(HttpStatusCode.Forbidden, "Host has disallowed control");

            var channelId = Context.User.FindFirst("ChannelId").Value;
            var hostConnectionId = _hostConnectionRepository.GetConnection(channelId);

            if (hostConnectionId == null)
                return new HubActionResult(HttpStatusCode.NotFound, "The channel has been closed by the host");

            await _hostHubContext.SendSlideShowCommand(hostConnectionId, new SlideShowActionMsg
            {
                Code = code,
                DateTime = DateTime.Now,
                UserId = userId
            });

            return new HubActionResult(HttpStatusCode.OK);
        }

        public async Task<HubActionResult> GetChannelState()
        {
            var channelId = Context.User.FindFirst("ChannelId").Value;

            var channel = await _applicationDbContext.Channels.FindAsync(channelId);

            if (channel == null) return new HubActionResult(HttpStatusCode.NotFound);

            var state = new {channel.SlideCount, channel.SlidePosition, channel.ChannelEnded};

            return new HubActionResult(HttpStatusCode.OK, state);
        }
    }
}