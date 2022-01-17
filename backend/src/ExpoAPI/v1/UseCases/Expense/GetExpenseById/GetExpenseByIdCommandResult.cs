namespace ExpoAPI.UseCases.Expense
{
    public class GetExpenseByIdCommandResult : CommandResultBase
    {
        public ExpenseContract? ExpenseContract { get; set; }
    }
}