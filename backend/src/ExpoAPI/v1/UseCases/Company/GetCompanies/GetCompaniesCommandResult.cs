using System.Collections.Generic;

namespace ExpoAPI.UseCases.Company
{
    public class GetCompaniesCommandResult : CommandResultBase
    {
        public List<CompanyContract?>? CompanyContracts { get; set; }
    }
}