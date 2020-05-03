using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using PowerPointRemote.WebApi.Hubs;
using PowerPointRemote.WebApi.Models;
using PowerPointRemote.WebApi.Models.Entity;

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
                .SendAsync("SlideShowCommand", slideShowCommand);
        }

        #endregion

        #region UserHub

        public static Task SendAllUsers(this IHubContext<UserHub> hubContext,
            IEnumerable<UserConnection> userConnections, string method, object[] args = null)
        {
            if (args == null)
                args = Array.Empty<object>();

            return Task.WhenAll(userConnections
                .Select(conn => hubContext.Clients.Client(conn.Id).SendCoreAsync(method, args)).ToList());
        }

        public static Task SendHostDisconnected(this IHubContext<UserHub> hubContext,
            IEnumerable<UserConnection> userConnections)
        {
            return hubContext.SendAllUsers(userConnections, "HostDisconnected");
        }

        public static Task SendHostConnected(this IHubContext<UserHub> hubContext,
            IEnumerable<UserConnection> userConnections)
        {
            return hubContext.SendAllUsers(userConnections, "HostConnected");
        }

        public static Task SendChannelEnded(this IHubContext<UserHub> hubContext,
            IEnumerable<UserConnection> userConnections)
        {
            return hubContext.SendAllUsers(userConnections, "ChannelEnded");
        }

        #endregion
    }
}