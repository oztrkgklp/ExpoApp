using MediatR;

namespace ExpoAPI.UseCases.Cost
{
    public class CreateCostCommand : IRequest<CreateCostCommandResult>
    {
        public CreateCostCommand(CostContract? contract)
        {
            Contract = contract;           
        }
        public CostContract? Contract { get; set; }
    }
}
