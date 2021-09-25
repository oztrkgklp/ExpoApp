using MediatR;

namespace ExpoAPI.UseCases.Company
{
    public class GetNumberOfNotEnteredCompaniesCommand : IRequest<GetNumberOfNotEnteredCompaniesCommandResult>
    {
    }
}
