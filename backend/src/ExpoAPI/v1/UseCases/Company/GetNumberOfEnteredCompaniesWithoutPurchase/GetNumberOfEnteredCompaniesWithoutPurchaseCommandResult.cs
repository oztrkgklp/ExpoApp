using System.Collections.Generic;

namespace ExpoAPI.UseCases.Company
{
    public class GetNumberOfEnteredCompaniesWithoutPurchaseCommandResult : CommandResultBase
    {
        public int? NumberOfEnteredCompaniesWithoutPurchase { get; set; }
    }
}