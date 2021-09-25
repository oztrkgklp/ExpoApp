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
    public class GetNumberOfAccommodationsCommandHandler : IRequestHandler<GetNumberOfAccommodationsCommand, GetNumberOfAccommodationsCommandResult>
    {
        private readonly ILogger<GetNumberOfAccommodationsCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public GetNumberOfAccommodationsCommandHandler(ILogger<GetNumberOfAccommodationsCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<GetNumberOfAccommodationsCommandResult> Handle(GetNumberOfAccommodationsCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var getNumberOfAccommodations = await _expoAPIQueryRepository.GetNumberOfAccommodationsAsync(cancellationToken);

                _logger.LogInformation("The number of accommodations has been fetched.");

                return new GetNumberOfAccommodationsCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    NumberOfAccommodations = getNumberOfAccommodations,
                    ReturnPath = $"/accommodations/count"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} ", ex.Message);

                return new GetNumberOfAccommodationsCommandResult()
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
                    ReturnPath = $"/accommodations/count"
                };
            }
        }
    }
}
