using System.Collections.Generic;

namespace ExpoAPI.UseCases.Purchase
{
    public class GetPurchasesCommandResult : CommandResultBase
    {
        public List<PurchaseContract?>? PurchaseContracts { get; set; }
    }
}