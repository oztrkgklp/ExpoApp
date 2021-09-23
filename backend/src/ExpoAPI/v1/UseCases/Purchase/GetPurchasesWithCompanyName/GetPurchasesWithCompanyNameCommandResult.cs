using System.Collections.Generic;

namespace ExpoAPI.UseCases.Purchase
{
    public class GetPurchasesWithCompanyNameCommandResult : CommandResultBase
    {
        public List<PurchaseWithNamesContract?>? PurchaseContracts { get; set; }
    }
}