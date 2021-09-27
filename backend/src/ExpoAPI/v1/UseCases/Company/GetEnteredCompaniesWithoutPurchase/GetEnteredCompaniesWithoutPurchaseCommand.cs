using MediatR;

namespace ExpoAPI.UseCases.Company
{
    public class GetEnteredCompaniesWithoutPurchaseCommand : IRequest<GetEnteredCompaniesWithoutPurchaseCommandResult>
    {
    }
}
