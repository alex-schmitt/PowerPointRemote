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

        public static async Task SendSetSlideShowDetailAsync(this HubConnection hubConnection,
            SlideShowDetailMsg slideShowDetailMsg)
        {
            await hubConnection.SendAsync(ServerMethods.SetSlideShowDetail, slideShowDetailMsg);
        }

        public static async Task SendSetCurrentSlideDetailAsync(this HubConnection hubConnection,
            SlideDetailMsg slideDetailMsg)
        {
            await hubConnection.SendAsync(ServerMethods.SetCurrentSlideDetail, slideDetailMsg);
        }

        public static async Task SendSetUserPermission(this HubConnection hubConnection,
            UserPermissionMsg userPermissionMsg)
        {
            await hubConnection.SendAsync(ServerMethods.SetUserPermission, userPermissionMsg);
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