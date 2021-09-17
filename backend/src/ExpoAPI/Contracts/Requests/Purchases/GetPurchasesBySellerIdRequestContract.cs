using System.Diagnostics.CodeAnalysis;

namespace ExpoAPI.Contracts.Requests
{
    [ExcludeFromCodeCoverage]
    public class GetPurchasesBySellerIdRequestContract
    {
        public int SellerID { get; set; }
    }
}