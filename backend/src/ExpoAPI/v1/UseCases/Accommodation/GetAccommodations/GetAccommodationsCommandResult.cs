using System.Collections.Generic;

namespace ExpoAPI.UseCases.Accommodation
{
    public class GetAccommodationsCommandResult : CommandResultBase
    {
        public List<AccommodationContract?>? AccommodationContracts { get; set; }
    }
}