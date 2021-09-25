using System.Collections.Generic;

namespace ExpoAPI.UseCases.Company
{
    public class GetNotEnteredCompaniesCommandResult : CommandResultBase
    {
        public List<CompanyContract?>? CompanyContracts { get; set; }
    }
}