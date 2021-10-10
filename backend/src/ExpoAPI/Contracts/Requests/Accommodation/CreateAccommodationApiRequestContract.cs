using System;
using System.Diagnostics.CodeAnalysis;

namespace ExpoAPI.Contracts.Requests
{
    [ExcludeFromCodeCoverage]
    public class CreateAccommodationApiRequestContract
    {
        public string? CompanyName { get; set; }
        public string? Hotel { get; set; }
        public DateTime? CheckIn { get; set; }
        public string? IsEntered { get; set; }
        public string? FirstGuest { get; set; }
        public string? SecondGuest { get; set; }
        public string? ThirdGuest { get; set; }
        public int? NumberOfGuests { get; set; }
        public string? GuestCompanyName { get; set; }
        public string? Phone { get; set; }
        public string? SNG { get; set; }
        public string? DBL { get; set; }
        public string? TRPL { get; set; }
        public string? QUAT { get; set; }
        public string? SNGCHD { get; set; }
        public string? DBLCHD { get; set; }
        public string? TRPLCHD { get; set; }
        public DateTime? CheckOut { get; set; }
        public string? SNG_ { get; set; }
        public string? DBL_ { get; set; }
        public string? TRPL_ { get; set; }
        public string? QUAT_ { get; set; }
        public string? SNG_CHD { get; set; }
        public string? DBL_CHD { get; set; }
        public string? TRPL_CHD { get; set; }
        public string? Description { get; set; }

    }
}