using MediatR;

namespace ExpoAPI.UseCases.Purchase
{
    public class GetPurchasesWithCompanyNameCommand : IRequest<GetPurchasesWithCompanyNameCommandResult>
    {
    }
}
