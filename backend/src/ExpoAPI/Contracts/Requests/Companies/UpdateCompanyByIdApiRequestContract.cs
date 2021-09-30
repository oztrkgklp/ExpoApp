
namespace ExpoAPI.Contracts.Requests
{
    public class UpdateCompanyByIdApiRequestContract
    {
        public string? CompanyName { get; set; }
        public string? Phone { get; set; }
        public string? EMail { get; set; }
        public decimal Endorsement { get; set; }    
        public string? IsEntered { get; set; }
        public string? IsGuest { get; set; }

    }
}