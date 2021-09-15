using MediatR;

namespace ExpoAPI.UseCases.Company
{
    public class GetCompanyByIdCommand : IRequest<GetCompanyByIdCommandResult>
    {
        public GetCompanyByIdCommand(int id)
        {
            CompanyID = id;
        }

        public int CompanyID { get; set; }
    }
}
