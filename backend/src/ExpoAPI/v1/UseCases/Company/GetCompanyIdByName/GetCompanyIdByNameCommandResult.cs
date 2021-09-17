namespace ExpoAPI.UseCases.Company
{
    public class GetCompanyIdByNameCommandResult : CommandResultBase
    {
        public int? CompanyID { get; set; }
    }
}