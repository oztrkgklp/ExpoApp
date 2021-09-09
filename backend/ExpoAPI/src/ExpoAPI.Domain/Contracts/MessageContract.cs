using System.Diagnostics.CodeAnalysis;

namespace ExpoAPI.Domain.Contracts
{
    [ExcludeFromCodeCoverage]
    public class MessageContract
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Code { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}