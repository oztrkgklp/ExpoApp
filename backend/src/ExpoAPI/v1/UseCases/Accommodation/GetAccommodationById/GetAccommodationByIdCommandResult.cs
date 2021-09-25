namespace ExpoAPI.UseCases.Accommodation
{
    public class GetAccommodationByIdCommandResult : CommandResultBase
    {
        public AccommodationContract? AccommodationContract { get; set; }
    }
}