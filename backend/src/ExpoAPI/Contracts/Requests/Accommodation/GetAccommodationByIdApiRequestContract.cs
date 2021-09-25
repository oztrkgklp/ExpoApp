
using System.Diagnostics.CodeAnalysis;

namespace ExpoAPI.Contracts.Requests
{
    [ExcludeFromCodeCoverage]
    public class GetAccommodationByIdApiRequestContract
    {
        public int AccommodationID { get; set; }
    }
}