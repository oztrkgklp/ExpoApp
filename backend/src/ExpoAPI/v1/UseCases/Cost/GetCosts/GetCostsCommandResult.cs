using System.Collections.Generic;

namespace ExpoAPI.UseCases.Cost
{
    public class GetCostsCommandResult : CommandResultBase
    {
        public List<CostContract?>? CostContracts { get; set; }
    }
}