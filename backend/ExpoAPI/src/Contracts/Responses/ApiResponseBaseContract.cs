using System.Text.Json.Serialization;

namespace ExpoAPI.Contracts.Responses
{
    public abstract class ApiResponseBaseContract<TContract>
    {
        [JsonPropertyName("instance")] public virtual string? Instance { get; set; }
        [JsonPropertyName("messages")] public virtual List<MessageContract>? Messages { get; set; }
        [JsonPropertyName("result")] public virtual TContract? Result { get; set; }

        [JsonPropertyName("success")] public virtual bool Success => !Messages?.Any() ?? true;
        [JsonPropertyName("returnPath")] public virtual string? ReturnPath { get; set; }
    }
}
