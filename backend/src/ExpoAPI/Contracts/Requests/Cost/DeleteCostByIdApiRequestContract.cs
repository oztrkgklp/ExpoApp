
using System.Diagnostics.CodeAnalysis;

namespace ExpoAPI.Contracts.Requests
{
    [ExcludeFromCodeCoverage]
    public class DeleteCostByIdApiRequestContract
    {
        public int CostID { get; set; }
    }
}