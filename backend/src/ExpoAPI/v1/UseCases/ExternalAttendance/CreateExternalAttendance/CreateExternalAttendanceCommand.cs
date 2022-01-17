using MediatR;

namespace ExpoAPI.UseCases.ExternalAttendance
{
    public class CreateExternalAttendanceCommand : IRequest<CreateExternalAttendanceCommandResult>
    {
        public CreateExternalAttendanceCommand(ExternalAttendanceContract? contract)
        {
            Contract = contract;           
        }
        public ExternalAttendanceContract? Contract { get; set; }
    }
}
