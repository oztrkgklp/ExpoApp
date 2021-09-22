using MediatR;

namespace ExpoAPI.UseCases.Company
{
    public class GetCompanyNameByIdCommand : IRequest<GetCompanyNameByIdCommandResult>
    {
        public GetCompanyNameByIdCommand(int companyID)
        {
            CompanyID = companyID;
        }

        public int CompanyID { get; set; }
    }
}
