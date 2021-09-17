using System.Diagnostics.CodeAnalysis;

namespace ExpoAPI.Contracts.Requests
{
    [ExcludeFromCodeCoverage]
    public class GetPurchasesByPurchaserIdRequestContract
    {
        public int PurchaserID { get; set; }
    }
}