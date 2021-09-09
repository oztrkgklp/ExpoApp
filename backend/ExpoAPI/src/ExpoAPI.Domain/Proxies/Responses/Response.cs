using System.Text.Json.Serialization;

namespace ExpoAPI.Domain.Proxies
{
    public class ExpoAPIResponse<T>
    {
        [JsonPropertyName("jsonrpc")] public string JSonRCP { get; set; }
        [JsonPropertyName("result")] public T Result { get; set; }
        [JsonPropertyName("error")] public ExpoAPIError Error { get; set; }
        [JsonPropertyName("id")] public string Id { get; set; }
    }
}