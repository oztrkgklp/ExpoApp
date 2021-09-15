using MediatR;

namespace ExpoAPI.UseCases.Purchase
{
    public class UpdatePurchaseByIdCommand : IRequest<UpdatePurchaseByIdCommandResult>
    {
        public UpdatePurchaseByIdCommand(PurchaseContract? contract)
        {
            Contract = contract;           
        }
        public PurchaseContract Contract { get; set; }
    }
}
