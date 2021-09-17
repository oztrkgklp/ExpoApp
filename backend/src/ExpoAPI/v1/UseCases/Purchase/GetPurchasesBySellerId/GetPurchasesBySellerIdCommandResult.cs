namespace ExpoAPI.UseCases.Purchase
{
    public class GetPurchasesBySellerIdCommandResult : CommandResultBase
    {
        public List<PurchaseContract?>? PurchaseContracts { get; set; }
    }
}