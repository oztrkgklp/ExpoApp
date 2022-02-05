
using System;

namespace ExpoAPI.Contracts.Requests
{
    public class UpdateExternalAttendanceByIdApiRequestContract
    {
        public int ExternalAttendanceID { get; set; }
        public string? NameSurname { get; set; }
        public int? TCID { get; set; }
        public int? NumberOfPeople { get; set; }
        public string? Phone { get; set; }
        public string? CompanyName { get; set; }
        public TimeSpan? EntranceTime { get; set; }
        public TimeSpan? ExitTime { get; set; }
        public TimeSpan? Occupancy { get; set; }
        public DateTime? EntranceDate { get; set; }
        public string? Description { get; set; }

    }
}