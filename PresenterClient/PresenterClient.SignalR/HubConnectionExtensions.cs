using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using PresenterClient.SignalR.Messages;
using PresenterClient.SignalR.Methods;

namespace PresenterClient.SignalR
{
    public static class HubConnectionExtensions
    {
        public static async Task SendChannelEndedAsync(this HubConnection hubConnection)
        {
            await hubConnection.SendAsync(ServerMethods.StopChannel);
        }

        public static async Task SendSetUserPermission(this HubConnection hubConnection,
            UserPermissionMsg userPermissionMsg)
        {
            await hubConnection.SendAsync(ServerMethods.SetUserPermission, userPermissionMsg);
        }

        public static async Task SendSetSlideShowCount(this HubConnection hubConnection,
            int count)
        {
            await hubConnection.SendAsync(ServerMethods.SetSlideCount, count);
        }

        public static async Task SendSetSlideShowPosition(this HubConnection hubConnection,
            int position)
        {
            await hubConnection.SendAsync(ServerMethods.SetSlidePosition, position);
        }

        public static void OnSlideShowActionReceived(this HubConnection hubConnection,
            Action<SlideShowActionMsg> action)
        {
            hubConnection.On(ClientMethods.SlideShowActionReceived, action);
        }

        public static void OnUserConnected(this HubConnection hubConnection, Action<ChannelUserMsg> action)
        {
            hubConnection.On(ClientMethods.UserConnected, action);
        }

        public static void OnUserDisconnected(this HubConnection hubConnection, Action<ChannelUserMsg> action)
        {
            hubConnection.On(ClientMethods.UserDisconnected, action);
        }
    }
}