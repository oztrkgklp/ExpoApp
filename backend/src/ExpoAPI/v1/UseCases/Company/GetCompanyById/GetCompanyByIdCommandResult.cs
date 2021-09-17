namespace ExpoAPI.UseCases.Company
{
    public class GetCompanyByIdCommandResult : CommandResultBase
    {
        public CompanyContract? CompanyContract { get; set; }
    }
}