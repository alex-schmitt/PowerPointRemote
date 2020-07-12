using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PresenterClient.NoteConverter
{
    public static class JsonSerializer
    {
        private static readonly JsonSerializerOptions JsonSerializerOptions;

        static JsonSerializer()
        {
            JsonSerializerOptions = new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        }

        public static string Serialize(List<DeltaOp> ops)
        {
            return System.Text.Json.JsonSerializer.Serialize(ops, JsonSerializerOptions);
        }
    }
}