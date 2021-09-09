using System.Diagnostics.CodeAnalysis;

namespace ExpoAPI.Domain.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class DomainException : Exception
    {
        public string Code { get; private set; }

        public DomainException(string code, string message)
            : base(message)
        {
            this.Code = code;
        }
    }
}