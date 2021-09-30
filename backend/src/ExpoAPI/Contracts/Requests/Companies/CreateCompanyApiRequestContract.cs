
using System.Diagnostics.CodeAnalysis;

namespace ExpoAPI.Contracts.Requests
{
    [ExcludeFromCodeCoverage]
    public class CreateCompanyApiRequestContract
    {
        public string? CompanyName { get; set; }
        public string? Phone { get; set; }
        public string? EMail { get; set; }
        public decimal Endorsement { get; set; } = 0;
        public string? IsEntered { get; set; }
        public string? IsGuest { get; set; }

    }
}