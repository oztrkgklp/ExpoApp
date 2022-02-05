using MediatR;

namespace ExpoAPI.UseCases.Expense
{
    public class DeleteExpenseByIdCommand : IRequest<DeleteExpenseByIdCommandResult>
    {
        public DeleteExpenseByIdCommand(int id)
        {
            ExpenseID = id;           
        }
        public int ExpenseID { get; set; }
    }
}
