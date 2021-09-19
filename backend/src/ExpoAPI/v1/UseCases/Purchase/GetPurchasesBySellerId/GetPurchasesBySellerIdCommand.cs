using MediatR;

namespace ExpoAPI.UseCases.Purchase
{
    public class GetPurchasesBySellerIdCommand : IRequest<GetPurchasesBySellerIdCommandResult>
    {
        public GetPurchasesBySellerIdCommand(int id)
        {
            SellerID = id;           
        }
        public int SellerID { get; set; }
    }
}
