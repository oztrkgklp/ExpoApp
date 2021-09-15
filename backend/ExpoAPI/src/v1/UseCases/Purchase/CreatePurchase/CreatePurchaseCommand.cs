using MediatR;

namespace ExpoAPI.UseCases.Purchase
{
    public class CreatePurchaseCommand : IRequest<CreatePurchaseCommandResult>
    {
        public CreatePurchaseCommand(PurchaseContract? contract)
        {
            Contract = contract;           
        }
        public PurchaseContract? Contract { get; set; }
    }
}
