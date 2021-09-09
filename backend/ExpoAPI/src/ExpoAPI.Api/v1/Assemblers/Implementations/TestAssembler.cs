using ExpoAPI.Api.Controllers.Contracts.Requests;
using ExpoAPI.Domain.Contracts;

namespace ExpoAPI.Api.Controllers.Assemblers.Implementations
{
    public class HostGroupAssembler : ITestAssembler
    {
        public TemplateContract ToCommand(ApiTestRequestContract contract)
        {
            return new TemplateContract()
            {
                Id = contract.Id
            };
        }
    }
}