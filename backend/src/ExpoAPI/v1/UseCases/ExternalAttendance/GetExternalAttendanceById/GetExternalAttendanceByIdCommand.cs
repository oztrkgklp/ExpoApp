using MediatR;

namespace ExpoAPI.UseCases.ExternalAttendance
{
    public class GetExternalAttendanceByIdCommand : IRequest<GetExternalAttendanceByIdCommandResult>
    {
        public GetExternalAttendanceByIdCommand(int id)
        {
            ExternalAttendanceId = id;
        }

        public int ExternalAttendanceId { get; set; }
    }
}
