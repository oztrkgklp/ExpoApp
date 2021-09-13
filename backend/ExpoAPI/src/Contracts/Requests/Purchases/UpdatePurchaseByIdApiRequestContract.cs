
namespace ExpoAPI.Contracts.Requests
{
    public class UpdatePurchaseByIdApiRequestContract
    {
        public int PurchaseID { get; set; }

        // Note that this property must be in XML format
        public string? Seller { get; set; }

        // Note that this property must be in XML format
        public string? Purchaser { get; set; }

        DateTime PurchaseDate { get; set; }

        // Note that this property must be in XML format
        public string? Info { get; set; }    
    }
}