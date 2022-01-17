namespace ExpoAPI.UseCases.Cost
{
    public class GetCostByIdCommandResult : CommandResultBase
    {
        public CostContract? CostContract { get; set; }
    }
}