using System;

namespace ExpoAPI.Contracts
{
    public class MessageContract
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string? Code { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Type { get; set; }
    }
}