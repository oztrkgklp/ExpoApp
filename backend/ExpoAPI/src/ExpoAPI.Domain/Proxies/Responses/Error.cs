using System.Text.Json.Serialization;

namespace ExpoAPI.Domain.Proxies
{
    public class ExpoAPIError
    {
        [JsonPropertyName("code")] public long Code { get; set; }
        [JsonPropertyName("message")] public string Message { get; set; }
        [JsonPropertyName("data")] public string Data { get; set; }
    }
}