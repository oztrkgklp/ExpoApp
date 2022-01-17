namespace ExpoAPI.UseCases.ExternalAttendance
{
    public class GetExternalAttendanceByIdCommandResult : CommandResultBase
    {
        public ExternalAttendanceContract? ExternalAttendanceContract { get; set; }
    }
}