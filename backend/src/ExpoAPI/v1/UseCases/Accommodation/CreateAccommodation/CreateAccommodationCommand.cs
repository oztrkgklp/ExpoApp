using MediatR;

namespace ExpoAPI.UseCases.Accommodation
{
    public class CreateAccommodationCommand : IRequest<CreateAccommodationCommandResult>
    {
        public CreateAccommodationCommand(AccommodationContract? contract)
        {
            Contract = contract;           
        }
        public AccommodationContract? Contract { get; set; }
    }
}
