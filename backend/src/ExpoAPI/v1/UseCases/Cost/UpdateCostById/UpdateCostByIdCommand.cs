using MediatR;

namespace ExpoAPI.UseCases.Cost
{
    public class UpdateCostByIdCommand : IRequest<UpdateCostByIdCommandResult>
    {
        public UpdateCostByIdCommand(CostContract? contract)
        {
            Contract = contract;           
        }
        public CostContract? Contract { get; set; }
    }
}
