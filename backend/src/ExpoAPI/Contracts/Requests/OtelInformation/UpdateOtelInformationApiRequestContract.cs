
using System.Diagnostics.CodeAnalysis;

namespace ExpoAPI.Contracts.Requests
{
    [ExcludeFromCodeCoverage]
    public class UpdateOtelInformationApiRequestContract
    {
        public int OtelInformationID { get; set; }
        public string? SNG { get; set; }
        public string? DBL { get; set; }
        public string? TRPL { get; set; }
        public string? QUAT { get; set; }
        public string? SNGCHD { get; set; }
        public string? DBLCHD { get; set; }
        public string? TRPLCHD { get; set; }

    }
}