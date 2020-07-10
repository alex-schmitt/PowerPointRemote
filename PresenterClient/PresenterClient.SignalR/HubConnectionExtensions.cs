using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using PresenterClient.SignalR.Messages;
using PresenterClient.SignalR.Methods;

namespace PresenterClient.SignalR
{
    public static class HubConnectionExtensions
    {
        public static async Task SendChannelStartedAsync(this HubConnection hubConnection)
        {
            await hubConnection.SendAsync(ServerMethods.StartChannel);
        }

        public static async Task SendChannelEndedAsync(this HubConnection hubConnection)
        {
            await hubConnection.SendAsync(ServerMethods.StopChannel);
        }

        public static async Task SendSlideShowDetailAsync(this HubConnection hubConnection,
            SlideShowDetail slideShowDetail)
        {
            await hubConnection.SendAsync(ServerMethods.UpdateSlideShowDetail, slideShowDetail);
        }

        public static async Task SendUserUpdateAsync(this HubConnection hubConnection, ChannelUser channelUser)
        {
            await hubConnection.SendAsync(ServerMethods.UpdateUser, channelUser);
        }

        public static void OnSlideShowActionReceived(this HubConnection hubConnection, Action<SlideShowAction> action)
        {
            hubConnection.On(ClientMethods.SlideShowActionReceived, action);
        }

        public static void OnUserConnected(this HubConnection hubConnection, Action<ChannelUser> action)
        {
            hubConnection.On(ClientMethods.UserConnected, action);
        }

        public static void OnUserDisconnected(this HubConnection hubConnection, Action<ChannelUser> action)
        {
            hubConnection.On(ClientMethods.UserDisconnected, action);
        }
    }
}