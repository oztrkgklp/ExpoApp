namespace ExpoAPI.UseCases.Purchase
{
    public class GetPurchaseByIdCommandResult : CommandResultBase
    {
        public PurchaseContract? PurchaseContract { get; set; }
    }
}