using MediatR;

namespace ExpoAPI.UseCases.Purchase
{
    public class GetPurchasesByPurchaserIdCommand : IRequest<GetPurchasesByPurchaserIdCommandResult>
    {
        public GetPurchasesByPurchaserIdCommand(int id)
        {
            PurchaserID = id;           
        }
        public int PurchaserID { get; set; }
    }
}
