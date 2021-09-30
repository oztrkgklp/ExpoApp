using System;

namespace ExpoAPI.UseCases.Purchase
{
    [Serializable]  
    public class PurchaseWithNamesContract
    {
        public int PurchaseID { get; set; }
        public string? SellerName { get; set; }
        public string? PurchaserName { get; set; }
        public string? Product { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal Amount { get; set; }
    }
}