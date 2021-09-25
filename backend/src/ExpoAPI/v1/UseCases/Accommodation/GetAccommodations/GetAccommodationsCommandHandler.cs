using System;
using System.Collections.Generic;
using System.Linq;
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
    public class GetAccommodationsCommandHandler : IRequestHandler<GetAccommodationsCommand, GetAccommodationsCommandResult>
    {
        private readonly ILogger<GetAccommodationsCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public GetAccommodationsCommandHandler(ILogger<GetAccommodationsCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<GetAccommodationsCommandResult> Handle(GetAccommodationsCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var getAccommodations = await _expoAPIQueryRepository.GetAccommodationsAsync(cancellationToken);

                _logger.LogInformation("The company data has been fetched.");

                var list = new List<AccommodationContract?>();
                foreach(var acc in getAccommodations)
                {
                    list.Add(new AccommodationContract()
                    {
                        AccommodationID = acc.AccommodationID,
                        CompanyName = acc.CompanyName,
                        Hotel = acc.Hotel,
                        CheckIn = DateTime.Parse(acc.CheckInDate.Value.ToShortDateString()+" "+acc.CheckInTime.Value.ToString()) ,
                        FirstGuest = acc.FirstGuest,
                        SecondGuest = acc.SecondGuest,
                        ThirdGuest = acc.ThirdGuest,
                        GuestCompanyName = acc.GuestCompanyName,
                        Phone = acc.Phone,
                        SNG = acc.SNG,
                        DBL = acc.DBL,
                        TRPL = acc.TRPL,
                        QUAT = acc.QUAT,
                        SNGCHD = acc.SNGCHD,
                        DBLCHD = acc.DBLCHD,
                        TRPLCHD = acc.TRPLCHD,
                        CheckOut = DateTime.Parse(acc.CheckOutDate.Value.ToShortDateString()+" "+acc.CheckOutTime.Value.ToString()),
                        _SNG = acc._SNG,
                        _DBL = acc._DBL,
                        _TRPL = acc._TRPL,
                        _QUAT = acc._QUAT,
                        _SNGCHD = acc._SNGCHD,
                        _DBLCHD = acc._DBLCHD,
                        _TRPLCHD = acc._TRPLCHD,
                        Description = acc.Description,
                    }
                    );
                }

                return new GetAccommodationsCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    AccommodationContracts = list,
                    ReturnPath = $"/accommodations"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} ", ex.Message);

                return new GetAccommodationsCommandResult()
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
                    ReturnPath = $"/accommodations"
                };
            }
        }
    }
}
