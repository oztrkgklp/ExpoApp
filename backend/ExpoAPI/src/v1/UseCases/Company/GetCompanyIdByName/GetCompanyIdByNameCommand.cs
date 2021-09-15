using MediatR;

namespace ExpoAPI.UseCases.Company
{
    public class GetCompanyIdByNameCommand : IRequest<GetCompanyIdByNameCommandResult>
    {
        public GetCompanyIdByNameCommand(string? name)
        {
            CompanyName = name;
        }

        public string? CompanyName { get; set; }
    }
}
