using System;

namespace ExpoAPI.UseCases.Purchase
{
    [Serializable]  
    public class PurchaseContract
    {
        public int PurchaseID { get; set; }
        public int SellerID { get; set; }
        public int PurchaserID { get; set; }
        public string? Product { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal Amount { get; set; }
    }
}