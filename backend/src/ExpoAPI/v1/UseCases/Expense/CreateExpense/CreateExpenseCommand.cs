using MediatR;

namespace ExpoAPI.UseCases.Expense
{
    public class CreateExpenseCommand : IRequest<CreateExpenseCommandResult>
    {
        public CreateExpenseCommand(ExpenseContract? contract)
        {
            Contract = contract;           
        }
        public ExpenseContract? Contract { get; set; }
    }
}
