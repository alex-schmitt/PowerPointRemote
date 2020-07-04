using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PresenterClient.Http
{
    public static class HttpContentExtensions
    {
        private static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
            {PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

        public static async Task<T> Deserialize<T>(this HttpContent content)
        {
            return await JsonSerializer.DeserializeAsync<T>(await content.ReadAsStreamAsync(), JsonSerializerOptions);
        }
    }
}