using System;

namespace ExpoAPI.UseCases.Accommodation
{
    public class AccommodationDBContract
    {
        public int AccommodationID { get; set; }
        public string? CompanyName { get; set; }
        public string? Hotel { get; set; }
        public DateTime? CheckInDate { get; set; }
        public TimeSpan? CheckInTime { get; set;}
        public string? FirstGuest { get; set; }
        public string? SecondGuest { get; set; }
        public string? ThirdGuest { get; set; }
        public string? GuestCompanyName { get; set; }
        public string? Phone { get; set; }
        public string? SNG { get; set; }
        public string? DBL { get; set; }
        public string? TRPL { get; set; }
        public string? QUAT { get; set; }
        public string? SNGCHD { get; set; }
        public string? DBLCHD { get; set; }
        public string? TRPLCHD { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public TimeSpan? CheckOutTime { get; set;}
        public string? _SNG { get; set; }
        public string? _DBL { get; set; }
        public string? _TRPL { get; set; }
        public string? _QUAT { get; set; }
        public string? _SNGCHD { get; set; }
        public string? _DBLCHD { get; set; }
        public string? _TRPLCHD { get; set; }
        public string? Description { get; set; }

    }
}