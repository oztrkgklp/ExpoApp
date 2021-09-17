using MediatR;

namespace ExpoAPI.UseCases.Company
{
    public class DeleteCompanyByIdCommand : IRequest<DeleteCompanyByIdCommandResult>
    {
        public DeleteCompanyByIdCommand(int id)
        {
            CompanyID = id;           
        }
        public int CompanyID { get; set; }
    }
}
