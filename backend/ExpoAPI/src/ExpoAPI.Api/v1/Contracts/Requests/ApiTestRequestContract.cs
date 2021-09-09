using System.Diagnostics.CodeAnalysis;

namespace ExpoAPI.Api.Controllers.Contracts.Requests
{
    [ExcludeFromCodeCoverage]
    public class ApiTestRequestContract
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}