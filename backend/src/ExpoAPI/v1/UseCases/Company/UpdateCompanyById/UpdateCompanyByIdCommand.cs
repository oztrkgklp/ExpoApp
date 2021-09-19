using MediatR;

namespace ExpoAPI.UseCases.Company
{
    public class UpdateCompanyByIdCommand : IRequest<UpdateCompanyByIdCommandResult>
    {
        public UpdateCompanyByIdCommand(CompanyContract? contract)
        {
            CompanyContract = contract;
        }

        public CompanyContract? CompanyContract { get; set; }
    }
}
