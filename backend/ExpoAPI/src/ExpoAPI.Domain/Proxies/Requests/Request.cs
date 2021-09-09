using System.Text.Json.Serialization;

namespace ExpoAPI.Domain.Proxies
{
    public class Request
    {
        [JsonPropertyName("jsonrpc")] public string JsonRPC { get; set; }
        [JsonPropertyName("method")] public string Method { get; set; }
        [JsonPropertyName("params")] public object Params { get; set; }
        [JsonPropertyName("id")] public string Id { get; set; }
        [JsonPropertyName("auth")] public string Auth { get; set; }

        public Request()
        {
            JsonRPC = "2.0";
        }
    }
}