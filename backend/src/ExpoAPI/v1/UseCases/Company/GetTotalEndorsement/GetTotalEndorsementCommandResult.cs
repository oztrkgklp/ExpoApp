using System.Collections.Generic;

namespace ExpoAPI.UseCases.Company
{
    public class GetTotalEndorsementCommandResult : CommandResultBase
    {
        public decimal? TotalEndorsement { get; set; }
    }
}