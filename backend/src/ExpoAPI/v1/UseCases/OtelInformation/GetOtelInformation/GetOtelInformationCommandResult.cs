
namespace ExpoAPI.UseCases.OtelInformation
{
    public class GetOtelInformationCommandResult : CommandResultBase
    {
        public OtelInformationContract? OtelInformationContract { get; set; }
    }
}