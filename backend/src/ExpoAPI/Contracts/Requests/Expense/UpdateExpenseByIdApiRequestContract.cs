
using System;

namespace ExpoAPI.Contracts.Requests
{
    public class UpdateExpenseByIdApiRequestContract
    {
        public int ExpenseID { get; set; }
        public decimal? Amount { get; set; }

    }
}