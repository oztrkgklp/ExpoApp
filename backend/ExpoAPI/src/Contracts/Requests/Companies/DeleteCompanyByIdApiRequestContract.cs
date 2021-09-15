
using System.Diagnostics.CodeAnalysis;

namespace ExpoAPI.Contracts.Requests
{
    [ExcludeFromCodeCoverage]
    public class DeleteCompanyByIdApiRequestContract
    {
        public int CompanyID { get; set; }
    }
}