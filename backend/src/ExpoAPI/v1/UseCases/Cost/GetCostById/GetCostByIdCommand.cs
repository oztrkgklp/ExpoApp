using MediatR;

namespace ExpoAPI.UseCases.Cost
{
    public class GetCostByIdCommand : IRequest<GetCostByIdCommandResult>
    {
        public GetCostByIdCommand(int id)
        {
            CostId = id;
        }

        public int CostId { get; set; }
    }
}
