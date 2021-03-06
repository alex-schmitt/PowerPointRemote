﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using PowerPointRemote.WebApi.Hubs;
using PowerPointRemote.WebApi.Models.Messages;

namespace PowerPointRemote.WebApi.Extensions
{
    public static class HubContextExtensions
    {
        #region HostHub

        public static Task SendUserConnected(this IHubContext<HostHub> hubContext, string hostConnectionId,
            ChannelUserMsg channelUserMsg)
        {
            return hubContext.Clients.Client(hostConnectionId).SendAsync("UserConnected", channelUserMsg);
        }

        public static Task SendUserDisconnected(this IHubContext<HostHub> hubContext, string hostConnectionId,
            Guid userId)
        {
            var message = new {Id = userId};
            return hubContext.Clients.Client(hostConnectionId).SendAsync("UserDisconnected", message);
        }

        public static Task SendSlideShowCommand(this IHubContext<HostHub> hubContext, string hostConnectionId,
            SlideShowActionMsg slideShowActionMsg)
        {
            return hubContext.Clients.Client(hostConnectionId)
                .SendAsync("SlideShowActionReceived", slideShowActionMsg);
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

        public static Task SendSlideCount(this IHubContext<UserHub> hubContext, string channelId, int count)
        {
            return hubContext.Clients.Group(channelId).SendAsync("SlideCountUpdated", count);
        }

        public static Task SendSetSlidePosition(this IHubContext<UserHub> hubContext, string channelId, int position)
        {
            return hubContext.Clients.Group(channelId).SendAsync("SlidePositionUpdated", position);
        }

        #endregion
    }
}