using System.Collections.Generic;

namespace ExpoAPI.UseCases.Company
{
    public class GetNumberOfEnteredCompaniesCommandResult : CommandResultBase
    {
        public int? NumberOfEnteredCompanies { get; set; }
    }
}