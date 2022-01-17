
using System;
using System.Diagnostics.CodeAnalysis;

namespace ExpoAPI.Contracts.Requests
{
    [ExcludeFromCodeCoverage]
    public class CreateExpenseApiRequestContract
    {
        public decimal? Amount { get; set; }

    }
}