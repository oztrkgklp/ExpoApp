
using System.Diagnostics.CodeAnalysis;

namespace ExpoAPI.Contracts.Requests
{
    [ExcludeFromCodeCoverage]
    public class UpdatePurchaseByIdApiRequestContract
    {
        public int PurchaseID { get; set; }

        public int SellerID { get; set; }

        public int PurchaserID { get; set; }

        public decimal Amount { get; set; }    
    }
}