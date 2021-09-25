using MediatR;

namespace ExpoAPI.UseCases.Accommodation
{
    public class DeleteAccommodationByIdCommand : IRequest<DeleteAccommodationByIdCommandResult>
    {
        public DeleteAccommodationByIdCommand(int id)
        {
            AccommodationID = id;           
        }
        public int AccommodationID { get; set; }
    }
}
