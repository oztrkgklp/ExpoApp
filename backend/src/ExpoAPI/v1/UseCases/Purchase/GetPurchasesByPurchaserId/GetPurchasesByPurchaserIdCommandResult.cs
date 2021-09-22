using System.Collections.Generic;

namespace ExpoAPI.UseCases.Purchase
{
    public class GetPurchasesByPurchaserIdCommandResult : CommandResultBase
    {
        public List<PurchaseContract?>? PurchaseContracts { get; set; }
    }
}