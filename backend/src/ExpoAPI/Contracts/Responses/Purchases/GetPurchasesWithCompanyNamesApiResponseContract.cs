using System.Collections.Generic;
using ExpoAPI.UseCases.Purchase;

namespace ExpoAPI.Contracts.Responses
{
    public class GetPurchasesWithCompanyNamesApiResponseContract : ApiResponseBaseContract<List<PurchaseWithNamesContract?>?>
    {
    }
}