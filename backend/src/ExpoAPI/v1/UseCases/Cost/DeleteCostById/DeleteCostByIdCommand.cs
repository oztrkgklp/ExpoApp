using MediatR;

namespace ExpoAPI.UseCases.Cost
{
    public class DeleteCostByIdCommand : IRequest<DeleteCostByIdCommandResult>
    {
        public DeleteCostByIdCommand(int id)
        {
            CostID = id;           
        }
        public int CostID { get; set; }
    }
}
