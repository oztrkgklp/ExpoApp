using MediatR;

namespace ExpoAPI.UseCases.Expense
{
    public class GetExpensesCommand : IRequest<GetExpensesCommandResult>
    {
    }
}
