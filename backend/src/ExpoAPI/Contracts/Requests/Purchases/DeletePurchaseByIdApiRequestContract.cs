
using System.Diagnostics.CodeAnalysis;

namespace ExpoAPI.Contracts.Requests
{
    [ExcludeFromCodeCoverage]
    public class DeletePurchaseByIdApiRequestContract
    {
        public int PurchaseID { get; set; }
    }
}