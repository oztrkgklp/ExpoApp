using System.Diagnostics.CodeAnalysis;

namespace ExpoAPI.Contracts.Requests
{
    [ExcludeFromCodeCoverage]
    public class GetExpenseByIdApiRequestContract
    {
        public int ExpenseID { get; set; }
    }
}