namespace ExpoAPI.UseCases.Company
{
    public class GetCompanyNameByIdCommandResult : CommandResultBase
    {
        public string? CompanyName { get; set; }
    }
}