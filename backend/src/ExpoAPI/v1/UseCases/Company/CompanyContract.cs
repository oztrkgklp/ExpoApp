
namespace ExpoAPI.UseCases.Company
{
    public class CompanyContract
    {
        public int CompanyID { get; set; }

        public string? CompanyName { get; set; }
        public string? Phone { get; set; }
        public string? EMail { get; set; }
        public decimal Endorsement { get; set; }
        public bool IsEntered { get; set; }
        public bool IsGuest { get; set; }

    }
}
