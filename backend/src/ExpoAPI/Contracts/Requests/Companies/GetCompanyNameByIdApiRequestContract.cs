using System.Diagnostics.CodeAnalysis;

namespace ExpoAPI.Contracts.Requests
{
    [ExcludeFromCodeCoverage]
    public class GetCompanyNameByIdApiRequestContract
    {
        public int CompanyID { get; set; }
    }
}