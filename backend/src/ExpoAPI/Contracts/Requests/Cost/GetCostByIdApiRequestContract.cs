using System.Diagnostics.CodeAnalysis;

namespace ExpoAPI.Contracts.Requests
{
    [ExcludeFromCodeCoverage]
    public class GetCostByIdApiRequestContract
    {
        public int CostID { get; set; }
    }
}