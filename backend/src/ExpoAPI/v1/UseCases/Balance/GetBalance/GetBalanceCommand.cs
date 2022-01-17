using MediatR;

namespace ExpoAPI.UseCases.Balance
{
    public class GetBalanceCommand : IRequest<GetBalanceCommandResult>
    {
    }
}
