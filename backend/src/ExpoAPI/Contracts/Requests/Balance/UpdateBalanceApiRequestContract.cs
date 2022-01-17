
using System.Diagnostics.CodeAnalysis;

namespace ExpoAPI.Contracts.Requests
{
    [ExcludeFromCodeCoverage]
    public class UpdateBalanceApiRequestContract
    {
        public int BalanceID { get; set; }
        public decimal? Amount { get; set; }

    }
}