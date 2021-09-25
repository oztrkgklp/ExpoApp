using System.Collections.Generic;

namespace ExpoAPI.UseCases.Company
{
    public class GetNumberOfNotEnteredCompaniesCommandResult : CommandResultBase
    {
        public int? NumberOfNotEnteredCompanies { get; set; }
    }
}