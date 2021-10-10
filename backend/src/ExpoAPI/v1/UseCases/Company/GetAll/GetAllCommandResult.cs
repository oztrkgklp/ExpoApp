using System.Collections.Generic;

namespace ExpoAPI.UseCases.Company
{
    public class GetAllCommandResult : CommandResultBase
    {
        public List<CompanyContract?>? CompanyContracts { get; set; }
    }
}