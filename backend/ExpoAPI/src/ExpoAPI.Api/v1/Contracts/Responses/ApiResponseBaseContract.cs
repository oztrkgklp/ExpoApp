using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using ExpoAPI.Domain.Contracts;

namespace ExpoAPI.Api.Controllers.Contracts.Responses
{
    [ExcludeFromCodeCoverage]
    public abstract class ApiResponseBaseContract<TContract>
    {
        [JsonPropertyName("instance")] public virtual string Instance { get; set; }
        [JsonPropertyName("messages")] public virtual List<MessageContract> Messages { get; set; }
        [JsonPropertyName("result")] public virtual TContract Result { get; set; }

        [JsonPropertyName("success")] public virtual bool Success =>  !Messages?.Any() ?? true;
        [JsonPropertyName("returnPath")] public virtual string ReturnPath { get; set; }
    }
}