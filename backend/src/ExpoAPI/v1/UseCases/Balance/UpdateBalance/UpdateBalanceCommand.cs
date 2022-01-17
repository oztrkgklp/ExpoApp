using MediatR;

namespace ExpoAPI.UseCases.Balance
{
    public class UpdateBalanceCommand : IRequest<UpdateBalanceCommandResult>
    {
        public UpdateBalanceCommand(BalanceContract? contract)
        {
            BalanceContract = contract;
        }

        public BalanceContract? BalanceContract { get; set; }
    }
}
