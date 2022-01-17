using MediatR;

namespace ExpoAPI.UseCases.ExternalAttendance
{
    public class DeleteExternalAttendanceByIdCommand : IRequest<DeleteExternalAttendanceByIdCommandResult>
    {
        public DeleteExternalAttendanceByIdCommand(int id)
        {
            ExternalAttendanceID = id;           
        }
        public int ExternalAttendanceID { get; set; }
    }
}
