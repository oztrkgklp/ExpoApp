using System.Collections.Generic;

namespace ExpoAPI.UseCases.Company
{
    public class GetGuestsCommandResult : CommandResultBase
    {
        public List<CompanyContract?>? CompanyContracts { get; set; }
    }
}