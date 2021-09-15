using System.Diagnostics.CodeAnalysis;

namespace ExpoAPI.Contracts.Requests
{
    [ExcludeFromCodeCoverage]
    public class CreatePurchaseApiRequestContract
    {
        public int SellerID { get; set; }

        public int PurchaserID { get; set; }

        public decimal Amount { get; set; }        
    }
}