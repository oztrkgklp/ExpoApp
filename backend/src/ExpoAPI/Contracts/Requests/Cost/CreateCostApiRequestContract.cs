
using System;
using System.Diagnostics.CodeAnalysis;

namespace ExpoAPI.Contracts.Requests
{
    [ExcludeFromCodeCoverage]
    public class CreateCostApiRequestContract
    {
        public int CostType { get; set; }
        public DateTime CostDate { get; set; }
        public string? Description { get; set; }
        public int PAX { get; set; }
        public decimal? TotalCost { get; set; }

    }
}