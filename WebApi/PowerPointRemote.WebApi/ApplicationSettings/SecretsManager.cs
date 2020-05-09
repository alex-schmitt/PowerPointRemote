using System.Text.Json;
using System.Threading.Tasks;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;

namespace PowerPointRemote.WebApi.ApplicationSettings
{
    public static class SecretsManager
    {
        public static async Task<T> GetSecret<T>(string secretId)
        {
            var request = new GetSecretValueRequest {SecretId = secretId};
            var client = new AmazonSecretsManagerClient();
            var response = await client.GetSecretValueAsync(request);

            return JsonSerializer.Deserialize<T>(response.SecretString);
        }
    }
}