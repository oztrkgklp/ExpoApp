using MediatR;

namespace ExpoAPI.UseCases.ExternalAttendance
{
    public class UpdateExternalAttendanceByIdCommand : IRequest<UpdateExternalAttendanceByIdCommandResult>
    {
        public UpdateExternalAttendanceByIdCommand(ExternalAttendanceContract? contract)
        {
            Contract = contract;           
        }
        public ExternalAttendanceContract? Contract { get; set; }
    }
}
