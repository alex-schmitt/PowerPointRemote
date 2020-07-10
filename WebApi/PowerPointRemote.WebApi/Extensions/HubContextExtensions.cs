using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using PowerPointRemote.WebApi.Hubs;
using PowerPointRemote.WebApi.Models;

namespace PowerPointRemote.WebApi.Extensions
{
    public static class HubContextExtensions
    {
        #region HostHub

        public static Task SendUserConnected(this IHubContext<HostHub> hubContext, string hostConnectionId,
            Guid userId, string userName)
        {
            var message = new {Id = userId, Name = userName};
            return hubContext.Clients.Client(hostConnectionId).SendAsync("UserConnected", message);
        }

        public static Task SendUserDisconnected(this IHubContext<HostHub> hubContext, string hostConnectionId,
            Guid userId)
        {
            var message = new {Id = userId};
            return hubContext.Clients.Client(hostConnectionId).SendAsync("UserDisconnected", message);
        }

        public static Task SendSlideShowCommand(this IHubContext<HostHub> hubContext, string hostConnectionId,
            SlideShowCommand slideShowCommand)
        {
            return hubContext.Clients.Client(hostConnectionId)
                .SendAsync("SlideShowActionReceived", slideShowCommand);
        }

        #endregion

        #region UserHub

        public static Task SendHostDisconnected(this IHubContext<UserHub> hubContext, string channelId)
        {
            return hubContext.Clients.Group(channelId).SendAsync("HostDisconnected");
        }

        public static Task SendHostConnected(this IHubContext<UserHub> hubContext, string channelId)
        {
            return hubContext.Clients.Group(channelId).SendAsync("HostConnected");
        }

        public static Task SendChannelEnded(this IHubContext<UserHub> hubContext, string channelId)
        {
            return hubContext.Clients.Group(channelId).SendAsync("ChannelEnded");
        }

        public static Task SendSlideShowDetail(this IHubContext<UserHub> hubContext,
            string channelId, SlideShowDetailUpdate slideShowDetailUpdate)
        {
            return hubContext.Clients.Group(channelId).SendAsync("SlideShowDetailUpdated", slideShowDetailUpdate);
        }

        #endregion
    }
}