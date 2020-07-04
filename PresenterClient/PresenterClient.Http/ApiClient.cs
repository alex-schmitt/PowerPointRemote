using System;
using System.Net.Http;
using System.Threading.Tasks;
using PresenterClient.Common;
using PresenterClient.Http.Response;

namespace PresenterClient.Http
{
    public static class ApiClient
    {
        private static readonly HttpClient HttpClient;

        static ApiClient()
        {
            HttpClient = new HttpClient();
            HttpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            HttpClient.BaseAddress = new Uri(ChannelApiAddress);
        }

        private static string ChannelApiAddress => Util.IsDebug ? "https://localhost:5001" : "https://api.ppremote.com";

        public static async Task<CreateChannelResponse> CreateChannel()
        {
            var response = await HttpClient.PostAsync("create-channel", null);
            var channel = await response.Content.Deserialize<CreateChannelResponse>();
            return channel;
        }
    }
}