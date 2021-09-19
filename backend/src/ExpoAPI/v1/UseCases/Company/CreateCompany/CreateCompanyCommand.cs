using MediatR;

namespace ExpoAPI.UseCases.Company
{
    public class CreateCompanyCommand : IRequest<CreateCompanyCommandResult>
    {
        public CreateCompanyCommand(CompanyContract? contract)
        {
            Contract = contract;           
        }
        public CompanyContract? Contract { get; set; }
    }
}
