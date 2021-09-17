using System.Diagnostics.CodeAnalysis;

namespace ExpoAPI.Contracts.Requests
{
    [ExcludeFromCodeCoverage]
    public class GetCompanyByIdApiRequestContract
    {
        public int CompanyID { get; set; }
    }
}