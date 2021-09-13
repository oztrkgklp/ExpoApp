namespace ExpoAPI.UseCases.Purchase
{
    [Serializable]  
    public class PurchaseContract
    {
        public int PurchaseID { get; set; }
        public string? Seller { get; set; }
        public string? Purchaser { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string? PurchaseInfo { get; set; }
    }
}