namespace ExpoAPI.UseCases.Purchase
{
    public class GetPurchaseByIdCommandResult : CommandResultBase
    {
        public PurchaseCommandResultContract? PurchaseContract { get; set; }
    }
}