using System.Collections.Generic;

namespace ExpoAPI.UseCases.Expense
{
    public class GetExpensesCommandResult : CommandResultBase
    {
        public List<ExpenseContract?>? ExpenseContracts { get; set; }
    }
}