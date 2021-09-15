using MediatR;

namespace ExpoAPI.UseCases.Purchase
{
    public class DeletePurchaseByIdCommand : IRequest<DeletePurchaseByIdCommandResult>
    {
        public DeletePurchaseByIdCommand(int id)
        {
            PurchaseID = id;           
        }
        public int PurchaseID { get; set; }
    }
}
