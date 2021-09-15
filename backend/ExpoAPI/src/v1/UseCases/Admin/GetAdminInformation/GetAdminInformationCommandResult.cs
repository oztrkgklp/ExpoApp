
namespace ExpoAPI.UseCases.Admin
{
    public class GetAdminInformationCommandResult : CommandResultBase
    {
        public AdminInformationContract? AdminInformationContract { get; set; }
    }
}