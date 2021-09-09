using ExpoAPI.Api.Controllers.Contracts.Requests;
using ExpoAPI.Domain.Contracts;

namespace ExpoAPI.Api.Controllers.Assemblers
{
    public interface ITestAssembler
    {
        TemplateContract ToCommand(ApiTestRequestContract contract);
    }
}