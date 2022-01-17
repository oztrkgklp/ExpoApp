using MediatR;

namespace ExpoAPI.UseCases.Expense
{
    public class GetExpenseByIdCommand : IRequest<GetExpenseByIdCommandResult>
    {
        public GetExpenseByIdCommand(int id)
        {
            ExpenseId = id;
        }

        public int ExpenseId { get; set; }
    }
}
