using MediatR;

namespace ExpoAPI.UseCases.Expense
{
    public class UpdateExpenseByIdCommand : IRequest<UpdateExpenseByIdCommandResult>
    {
        public UpdateExpenseByIdCommand(ExpenseContract? contract)
        {
            Contract = contract;           
        }
        public ExpenseContract? Contract { get; set; }
    }
}
