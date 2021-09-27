using System.Collections.Generic;

namespace ExpoAPI.UseCases.Company
{
    public class GetEnteredCompaniesCommandResult : CommandResultBase
    {
        public List<CompanyContract?>? CompanyContracts { get; set; }
    }
}