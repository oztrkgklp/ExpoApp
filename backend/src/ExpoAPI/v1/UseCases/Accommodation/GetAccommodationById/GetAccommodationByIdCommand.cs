using MediatR;

namespace ExpoAPI.UseCases.Accommodation
{
    public class GetAccommodationByIdCommand : IRequest<GetAccommodationByIdCommandResult>
    {
        public GetAccommodationByIdCommand(int id)
        {
            AccommodationID = id;
        }

        public int AccommodationID { get; set; }
    }
}
