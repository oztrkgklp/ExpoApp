using MediatR;

namespace ExpoAPI.UseCases.Company
{
    public class GetNotEnteredCompaniesCommand : IRequest<GetNotEnteredCompaniesCommandResult>
    {
    }
}
