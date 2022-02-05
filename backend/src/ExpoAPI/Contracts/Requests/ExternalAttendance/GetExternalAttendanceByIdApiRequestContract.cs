using System.Diagnostics.CodeAnalysis;

namespace ExpoAPI.Contracts.Requests
{
    [ExcludeFromCodeCoverage]
    public class GetExternalAttendanceByIdApiRequestContract
    {
        public int ExternalAttendanceID { get; set; }
    }
}