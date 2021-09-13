using System.Diagnostics.CodeAnalysis;

namespace ExpoAPI.Contracts.Requests
{
    [ExcludeFromCodeCoverage]
    public class GetPurchaseByIdApiRequestContract
    {
        public int PurchaseID { get; set; }
    }
}