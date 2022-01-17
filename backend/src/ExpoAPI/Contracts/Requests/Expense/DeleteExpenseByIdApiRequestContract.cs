
using System.Diagnostics.CodeAnalysis;

namespace ExpoAPI.Contracts.Requests
{
    [ExcludeFromCodeCoverage]
    public class DeleteExpenseByIdApiRequestContract
    {
        public int ExpenseID { get; set; }
    }
}