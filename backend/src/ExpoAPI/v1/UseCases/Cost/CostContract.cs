using System;

namespace ExpoAPI.UseCases.Cost
{
    public class CostContract
    {
        public int CostID { get; set; }
        public int CostType { get; set; }
        public DateTime CostDate { get; set; }
        public string? Description { get; set; }
        public int PAX { get; set; }
        public decimal? TotalCost { get; set; }
    }
}