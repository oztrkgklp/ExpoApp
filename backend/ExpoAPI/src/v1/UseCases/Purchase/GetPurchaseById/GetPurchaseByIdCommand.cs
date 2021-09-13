using MediatR;

namespace ExpoAPI.UseCases.Purchase
{
    public class GetPurchaseByIdCommand : IRequest<GetPurchaseByIdCommandResult>
    {
        public GetPurchaseByIdCommand(int id)
        {
            PurchaseID = id;           
        }
        public int PurchaseID { get; set; }
    }
}
