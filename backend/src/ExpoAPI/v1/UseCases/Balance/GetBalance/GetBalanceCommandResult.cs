
namespace ExpoAPI.UseCases.Balance
{
    public class GetBalanceCommandResult : CommandResultBase
    {
        public BalanceContract? BalanceContract { get; set; }
    }
}