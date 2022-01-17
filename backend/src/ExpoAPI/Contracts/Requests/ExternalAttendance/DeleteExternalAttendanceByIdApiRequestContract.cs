
using System.Diagnostics.CodeAnalysis;

namespace ExpoAPI.Contracts.Requests
{
    [ExcludeFromCodeCoverage]
    public class DeleteExternalAttendanceByIdApiRequestContract
    {
        public int ExternalAttendanceID { get; set; }
    }
}