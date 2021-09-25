using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ExpoAPI.Contracts;
using ExpoAPI.ErrorCodes;
using ExpoAPI.Infrastructure.Adapters;
using ExpoAPI.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ExpoAPI.UseCases.Accommodation
{
    public class GetAccommodationByIdCommandHandler : IRequestHandler<GetAccommodationByIdCommand, GetAccommodationByIdCommandResult>
    {
        private readonly ILogger<GetAccommodationByIdCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public GetAccommodationByIdCommandHandler(ILogger<GetAccommodationByIdCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<GetAccommodationByIdCommandResult> Handle(GetAccommodationByIdCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var getAccommodationById = await _expoAPIQueryRepository.GetAccommodationByIdAsync(command.AccommodationID,cancellationToken);

                _logger.LogInformation("The accommodation with id {@AccommodationId} has been deleted.",command.AccommodationID);

                var accommodation = new AccommodationContract()
                {
                    AccommodationID = getAccommodationById.AccommodationID,
                    CompanyName = getAccommodationById.CompanyName,
                    Hotel = getAccommodationById.Hotel,
                    CheckIn = DateTime.Parse(getAccommodationById.CheckInDate.Value.ToShortDateString()+" "+getAccommodationById.CheckInTime.Value.ToString()) ,
                    FirstGuest = getAccommodationById.FirstGuest,
                    SecondGuest = getAccommodationById.SecondGuest,
                    ThirdGuest = getAccommodationById.ThirdGuest,
                    GuestCompanyName = getAccommodationById.GuestCompanyName,
                    Phone = getAccommodationById.Phone,
                    SNG = getAccommodationById.SNG,
                    DBL = getAccommodationById.DBL,
                    TRPL = getAccommodationById.TRPL,
                    QUAT = getAccommodationById.QUAT,
                    SNGCHD = getAccommodationById.SNGCHD,
                    DBLCHD = getAccommodationById.DBLCHD,
                    TRPLCHD = getAccommodationById.TRPLCHD,
                    CheckOut = DateTime.Parse(getAccommodationById.CheckOutDate.Value.ToShortDateString()+" "+getAccommodationById.CheckOutTime.Value.ToString()),
                    _SNG = getAccommodationById._SNG,
                    _DBL = getAccommodationById._DBL,
                    _TRPL = getAccommodationById._TRPL,
                    _QUAT = getAccommodationById._QUAT,
                    _SNGCHD = getAccommodationById._SNGCHD,
                    _DBLCHD = getAccommodationById._DBLCHD,
                    _TRPLCHD = getAccommodationById._TRPLCHD,
                    Description = getAccommodationById.Description,
                };


                return new GetAccommodationByIdCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    AccommodationContract = accommodation,
                    ReturnPath = $"/accommodations/{command.AccommodationID}"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} -> @{Id}", ex.Message,command.AccommodationID);

                return new GetAccommodationByIdCommandResult()
                {
                    ValidateState = ValidationState.NotAcceptable,
                    Messages = new List<MessageContract>()
                    {
                        new MessageContract()
                        {
                            Title = "Invalid Request",
                            Content = DomainErrorCodes.EDExpoAPI10000,
                            Code = nameof(DomainErrorCodes.EDExpoAPI10000),
                            Type = nameof(MessageType.Error)
                        }
                    },
                    ReturnPath = $"/accommodations/{command.AccommodationID}"
                };
            }
        }
    }
}
