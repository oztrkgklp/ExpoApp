namespace ExpoAPI.UseCases.Purchase
{
    [Serializable]  
    public class PurchaseContract
    {
        public int PurchaseID { get; set; }
        public object? Seller { get; set; }
        public object? Purchaser { get; set; }
        public DateTime PurchaseDate { get; set; }
        public object? PurchaseInfo { get; set; }
    }
}