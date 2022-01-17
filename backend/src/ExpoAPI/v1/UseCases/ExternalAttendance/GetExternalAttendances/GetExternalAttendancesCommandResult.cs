using System.Collections.Generic;

namespace ExpoAPI.UseCases.ExternalAttendance
{
    public class GetExternalAttendancesCommandResult : CommandResultBase
    {
        public List<ExternalAttendanceContract?>? ExternalAttendanceContracts { get; set; }
    }
}