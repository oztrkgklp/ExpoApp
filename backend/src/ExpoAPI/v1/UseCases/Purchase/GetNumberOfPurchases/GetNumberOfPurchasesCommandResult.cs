using System.Collections.Generic;

namespace ExpoAPI.UseCases.Purchase
{
    public class GetNumberOfPurchasesCommandResult : CommandResultBase
    {
        public int? NumberOfPurchases { get; set; }
    }
}