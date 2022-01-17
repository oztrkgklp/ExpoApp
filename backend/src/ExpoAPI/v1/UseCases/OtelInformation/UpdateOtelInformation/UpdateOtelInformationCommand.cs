using MediatR;

namespace ExpoAPI.UseCases.OtelInformation
{
    public class UpdateOtelInformationCommand : IRequest<UpdateOtelInformationCommandResult>
    {
        public UpdateOtelInformationCommand(OtelInformationContract? contract)
        {
            OtelInformationContract = contract;
        }

        public OtelInformationContract? OtelInformationContract { get; set; }
    }
}
