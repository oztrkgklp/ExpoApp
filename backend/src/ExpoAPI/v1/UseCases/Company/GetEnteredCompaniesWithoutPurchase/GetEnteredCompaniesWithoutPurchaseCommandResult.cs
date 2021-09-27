using System.Collections.Generic;

namespace ExpoAPI.UseCases.Company
{
    public class GetEnteredCompaniesWithoutPurchaseCommandResult : CommandResultBase
    {
        public List<CompanyContract?>? CompanyContracts { get; set; }
    }
}