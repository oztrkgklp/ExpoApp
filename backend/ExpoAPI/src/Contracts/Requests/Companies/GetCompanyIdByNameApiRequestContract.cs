using System.Diagnostics.CodeAnalysis;

namespace ExpoAPI.Contracts.Requests
{
    [ExcludeFromCodeCoverage]
    public class GetCompanyIdByNameApiRequestContract
    {
        public string? CompanyName { get; set; }
    }
}