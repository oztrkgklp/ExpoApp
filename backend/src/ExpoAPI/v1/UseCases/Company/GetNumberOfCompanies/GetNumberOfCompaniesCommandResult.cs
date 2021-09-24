using System.Collections.Generic;

namespace ExpoAPI.UseCases.Company
{
    public class GetNumberOfCompaniesCommandResult : CommandResultBase
    {
        public int? NumberOfCompanies { get; set; }
    }
}