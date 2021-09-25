using MediatR;

namespace ExpoAPI.UseCases.Accommodation
{
    public class UpdateAccommodationByIdCommand : IRequest<UpdateAccommodationByIdCommandResult>
    {
        public UpdateAccommodationByIdCommand(AccommodationContract? contract)
        {
            Contract = contract;           
        }
        public AccommodationContract? Contract { get; set; }
    }
}
