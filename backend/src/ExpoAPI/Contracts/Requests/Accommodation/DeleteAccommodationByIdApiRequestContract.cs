
using System.Diagnostics.CodeAnalysis;

namespace ExpoAPI.Contracts.Requests
{
    [ExcludeFromCodeCoverage]
    public class DeleteAccommodationByIdApiRequestContract
    {
        public int AccommodationID { get; set; }
    }
}