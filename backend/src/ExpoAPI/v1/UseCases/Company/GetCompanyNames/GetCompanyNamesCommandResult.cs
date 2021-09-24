using System.Collections.Generic;

namespace ExpoAPI.UseCases.Company
{
    public class GetCompanyNamesCommandResult : CommandResultBase
    {
        public List<string?>? CompanyNames { get; set; }
    }
}